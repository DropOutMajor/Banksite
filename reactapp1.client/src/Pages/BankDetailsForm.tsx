import React, { useState } from 'react';
import axios from 'axios';

type BankDetailsFormProps = {
    paymentId: number;
};

const BankDetailsForm: React.FC<BankDetailsFormProps> = ({ paymentId }) => {
    const [accountNumber, setAccountNumber] = useState('');
    const [swiftCode, setSwiftCode] = useState('');
    const [message, setMessage] = useState('');

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            const payload = {
                paymentId: paymentId,
                accountNumber,
                swiftCode
            };

            const response = await axios.post('/api/bankdetails', payload);
            setMessage(response.data.message);
        } catch (err) {
            setMessage("Failed to submit bank details.");
            console.error(err);
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <h3>Enter Bank Details</h3>

            <div>
                <label>Account Number</label>
                <input
                    type="text"
                    value={accountNumber}
                    onChange={(e) => setAccountNumber(e.target.value)}
                    required
                />
            </div>

            <div>
                <label>SWIFT Code</label>
                <input
                    type="text"
                    value={swiftCode}
                    onChange={(e) => setSwiftCode(e.target.value)}
                    required
                />
            </div>

            <button type="submit">Submit</button>
            <p>{message}</p>
        </form>
    );
};

export default BankDetailsForm;
