import { useState } from 'react';
import '../App.css';

function PaymentForm() {
    const [amount, setAmount] = useState('');
    const [currency, setCurrency] = useState('USD');
    const [bank, setBank] = useState('Standard Bank');
    const [message, setMessage] = useState('');

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        // Replace this URL with your backend API endpoint
        const response = await fetch('https://localhost:5001/api/payment', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ amount, currency, bank }),
        });

        if (response.ok) {
            setMessage('Payment processed successfully!');
            setAmount('');
        } else {
            const error = await response.text();
            setMessage(`Payment failed: ${error}`);
        }
    };

    return (
        <div>
            <h1>Make a Payment</h1>
            <form onSubmit={handleSubmit} className="form">
                <div>
                    <label>Amount:</label>
                    <input
                        type="number"
                        value={amount}
                        onChange={(e) => setAmount(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>Currency:</label>
                    <select value={currency} onChange={(e) => setCurrency(e.target.value)}>
                        <option value="USD">USD</option>
                        <option value="ZAR">ZAR</option>
                        <option value="EUR">EUR</option>
                    </select>
                </div>
                <div>
                    <label>Bank:</label>
                    <select value={bank} onChange={(e) => setBank(e.target.value)}>
                        <option value="Standard Bank">Standard Bank</option>
                        <option value="FNB">FNB</option>
                        <option value="ABSA">ABSA</option>
                        <option value="Capitec">Capitec</option>
                    </select>
                </div>
                <button type="submit">Submit Payment</button>
            </form>
            {message && <p>{message}</p>}
        </div>
    );
}

export default PaymentForm;
