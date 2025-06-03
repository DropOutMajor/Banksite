// src/Pages/EmployeePortal.tsx
import { useEffect, useState } from 'react';

interface Payment {
    id: number;
    amount: number;
    currency: string;
    bank: string;
    isApproved: boolean;
    approvedAt?: string;
}

export default function EmployeePortal() {
    const [payments, setPayments] = useState<Payment[]>([]);
    const [error, setError] = useState<string>('');

    useEffect(() => {
        fetch('/paymentform', {
            method: 'GET',
            headers: { 'Content-Type': 'application/json' },
            credentials: 'include',
        })
            .then(async (response) => {
                if (!response.ok) throw new Error('Failed to fetch payments');
                const data = await response.json();
                setPayments(data);
            })
            .catch((err) => {
                setError(err instanceof Error ? err.message : 'Unknown error');
            });
    }, []);

    const approve = async (id: number) => {
        try {
            const res = await fetch(`/paymentform/${id}/approve`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                credentials: 'include',
            });
            if (!res.ok) throw new Error('Approve failed');
            setPayments((prev) =>
                prev.map((p) =>
                    p.id === id ? { ...p, isApproved: true, approvedAt: new Date().toISOString() } : p
                )
            );
        } catch (err) {
            setError(err instanceof Error ? err.message : 'Unknown error');
        }
    };

    const reject = async (id: number) => {
        try {
            const res = await fetch(`/paymentform/${id}/reject`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                credentials: 'include',
            });
            if (!res.ok) throw new Error('Reject failed');
            setPayments((prev) => prev.filter((p) => p.id !== id));
        } catch (err) {
            setError(err instanceof Error ? err.message : 'Unknown error');
        }
    };

    return (
        <div className="containerbox">
            <h3>Employee Payment Portal</h3>
            {error && <p className="error">{error}</p>}
            <table>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Amount</th>
                        <th>Currency</th>
                        <th>Bank</th>
                        <th>Status</th>
                        <th>Approved At</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {payments.map((p) => (
                        <tr key={p.id}>
                            <td>{p.id}</td>
                            <td>{p.amount}</td>
                            <td>{p.currency}</td>
                            <td>{p.bank}</td>
                            <td>{p.isApproved ? 'Approved' : 'Pending'}</td>
                            <td>{p.approvedAt ? new Date(p.approvedAt).toLocaleString() : '-'}</td>
                            <td>
                                {!p.isApproved && (
                                    <>
                                        <button onClick={() => approve(p.id)}>Approve</button>
                                        <button onClick={() => reject(p.id)}>Reject</button>
                                    </>
                                )}
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}
