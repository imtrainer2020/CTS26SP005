import React, { useState } from 'react';
import axios from 'axios';
//import { useNavigate } from 'react-router-dom';

const API_BASE_URL = "https://localhost:7123/api/Users";

export default function ForgetPassword() {
    //const navigate = useNavigate(); // Helper to send user back to login page

    // State Management
    const [email, setEmail] = useState('');
    const [newPassword, setNewPassword] = useState('');
    const [isEmailVerified, setIsEmailVerified] = useState(false);
    const [message, setMessage] = useState({ text: '', type: '' });

    // STEP 1: Check if email exists
    const checkEmail = async () => {
        try {
            const response = await axios.get<boolean>(`${API_BASE_URL}/exists/${email}`);

            if (response.data === true) {
                setIsEmailVerified(true);
                setMessage({ text: "Email verified. Enter your new password.", type: "alert-success" });
            } else {
                setMessage({ text: "This email does not exist in our records.", type: "alert-danger" });
            }
        } catch (err) {
            setMessage({ text: "Error checking email.", type: "alert-danger" });
        }
    };

    // STEP 2: Reset the password
    const handleReset = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            await axios.put(`${API_BASE_URL}/reset-password`, null, {
                params: { email, newPassword }
            });
            alert("Password Reset Successful!");
            //navigate('/'); //// Redirect back to Login page
        } catch (err) {
            setMessage({ text: "Failed to reset password.", type: "alert-danger" });
        }
    };

    return (
        <div className="container d-flex justify-content-center align-items-center vh-100">
            <div className="card shadow p-4" style={{ width: '400px' }}>
                <h3 className="text-center mb-4">Reset Password</h3>

                {message.text && <div className={`alert ${message.type} py-2`}>{message.text}</div>}

                {!isEmailVerified ? (
                    // Initial View: Email Check
                    <div>
                        <div className="mb-3">
                            <label className="form-label">Enter registered Email</label>
                            <input type="email" className="form-control" value={email}
                                onChange={(e) => setEmail(e.target.value)} />
                        </div>
                        <button className="btn btn-primary w-100" onClick={checkEmail}>Verify Email</button>
                    </div>
                ) : (
                    // Second View: Reset Form
                    <form onSubmit={handleReset}>
                        <div className="mb-3">
                            <label className="form-label">New Password</label>
                                <input type="password" className="form-control" value={newPassword}
                                    onChange={(e) => setNewPassword(e.target.value)} required />
                        </div>
                        <button type="submit" className="btn btn-success w-100">Update Password</button>
                    </form>
                )}

                <button className="btn btn-link w-100 mt-3" onClick={() => navigate('/')}>Back to Login</button>
            </div>
        </div>
    );
};