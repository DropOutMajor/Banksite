import { useState } from 'react';
import '../App.css';

function PaymentForm() {
    const [amount, setAmount] = useState('');
    const [currency, setCurrency] = useState('USD');
    const [bank, setBank] = useState('Standard Bank');
    const [message, setMessage] = useState('');

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        try {
            const response = await fetch('/paymentform', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ amount, currency, bank }),
            });

            if (response.ok) {
                setMessage('Payment processed successfully!');
                setAmount('');
                setCurrency('USD');
                setBank('Standard Bank');
            } else {
                const errorText = await response.text();
                setMessage(`Payment failed: ${errorText}`);
            }
        } catch (error) {
            setMessage(`Network error: ${error}`);
        }
    };

    return (
        <div className="containerbox">
            <h3>Make a Payment</h3>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="amount">Amount:</label>
                    <input
                        type="number"
                        id="amount"
                        value={amount}
                        onChange={(e) => setAmount(e.target.value)}
                        required
                    />
                </div>

                <div>
                    <label htmlFor="currency">Currency:</label>
                    <select
                        id="currency"
                        value={currency}
                        onChange={(e) => setCurrency(e.target.value)}
                    >
                        <option value="USD">USD</option>
                        <option value="ZAR">ZAR</option>
                        <option value="EUR">EUR</option>
                    </select>
                </div>

                <div>
                    <label htmlFor="bank">Bank:</label>
                    <select
                        id="bank"
                        value={bank}
                        onChange={(e) => setBank(e.target.value)}
                    >
                        <option value="Standard Bank">Standard Bank</option>
                        <option value="FNB">FNB</option>
                        <option value="ABSA">ABSA</option>
                    </select>
                </div>

                <button type="submit">Submit Payment</button>
            </form>

            {message && <p className="message">{message}</p>}
        </div>
    );
}

export default PaymentForm;
