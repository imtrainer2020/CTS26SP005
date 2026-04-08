import React, { useState, ChangeEvent, FormEvent } from 'react';
import axios from 'axios';
import type { User } from '../models/User';
import { useNavigate } from 'react-router-dom';

const API_BASE_URL = "https://localhost:7143/api/Users";
export default function LoginRegister() {
    const navigate = useNavigate();

    //UI State
    const [isLogin, setIsLogin] = useState<boolean>(true);
    const [message, setMessage] = useState<{ text: string; type: string }>({ text: '', type: '' });

    // Form State
    const [email, setEmail] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [confirmPassword, setConfirmPassword] = useState<string>('');

    // 1. Handle Login
    const onLogin = async (e: FormEvent) => {
        e.preventDefault();
        try {
            const response = await axios.get<User>(`${API_BASE_URL}/validate`, {
                params: { email, password }
            });

            console.log(response);
            const res = response.data;
            // Guard against userId possibly being undefined per the User type
            if (res != null && typeof res.userId === 'number' && res.userId > 0) {
                if (res.role.toLowerCase() == 'admin')
                    navigate('/admin-dashboard');
                else
                    navigate('/user-dashboard');
            } else {
                setMessage({ text: "Invalid Email or Password.", type: "alert-danger" });
            }
        } catch (err) {
            setMessage({ text: "API Error. Is the backend running?", type: "alert-danger" });
        }
    };

    // 2. Handle Registration
    const onRegister = async (e: FormEvent) => {
        e.preventDefault();

        // VALIDATION: Check if passwords match before calling API
        if (password !== confirmPassword) {
            setMessage({ text: "Passwords do not match!", type: "alert-danger" });
            return; // Stop the function here
        }

        try {
            const newUser: User = { email, password, role: "User" };
            //await axios.post(API_BASE_URL, newUser);

            const response = await axios.post(API_BASE_URL, newUser);
            console.log(response);
            if (response != null && response.data.length > 0) {
                const res = response.data;

                if (res.toLowerCase() == 'success') {
                    setMessage({ text: "Account created! You can now login.", type: "alert-success" });

                    // Clear passwords and switch to login
                    setPassword('');
                    setConfirmPassword('');
                    setIsLogin(true);
                }
                else if (res.toLowerCase() == 'exist')
                    setMessage({ text: "User Already Exist", type: "alert-danger" });
                else
                    setMessage({ text: "Registration failed.", type: "alert-danger" });
            }
            else
                setMessage({ text: "Registration failed.", type: "alert-danger" });
        } catch (err) {
            setMessage({ text: "Registration failed.", type: "alert-danger" });
        }
    };

    return (
        <div className="container d-flex justify-content-center align-items-center vh-100">
            <div className="card shadow-lg p-4" style={{ width: '400px' }}>
                <h2 className="text-center mb-4">{isLogin ? 'Sign In' : 'Create Account'}</h2>

                {message.text && (
                    <div className={`alert ${message.type} py-2 text-center`}>{message.text}</div>
                )}

                <form onSubmit={isLogin ? onLogin : onRegister} style={{ textAlign: 'left' }}>
                    <div className="mb-3">
                        <label className="form-label">Email address</label>
                        <input
                            type="email"
                            className="form-control"
                            value={email}
                            onChange={(e: ChangeEvent<HTMLInputElement>) => setEmail(e.target.value)}
                            required
                        />
                    </div>
                    <div className="mb-3">
                        <label className="form-label">Password</label>
                        <input
                            type="password"
                            className="form-control"
                            value={password}
                            onChange={(e: ChangeEvent<HTMLInputElement>) => setPassword(e.target.value)}
                            required
                        />
                    </div>

                    {/* ONLY SHOW CONFIRM PASSWORD IF NOT IN LOGIN MODE */}
                    {!isLogin && (
                        <div className="mb-3">
                            <label className="form-label">Confirm Password</label>
                            <input
                                type="password"
                                className="form-control"
                                value={confirmPassword}
                                onChange={(e: ChangeEvent<HTMLInputElement>) => setConfirmPassword(e.target.value)}
                                required
                            />
                        </div>
                    )}

                    {isLogin && (
                        <div className="text-end mb-3">
                            <button type="button" className="btn btn-link p-0 text-decoration-none"
                                onClick={() => navigate("/forgot-password")}>

                                Forgot Password?
                            </button>
                        </div>
                    )}

                    <button type="submit" className="btn btn-primary w-100 py-2">
                        {isLogin ? 'Login' : 'Register'}
                    </button>
                </form>

                <div className="text-center mt-4">
                    <span>{isLogin ? "Don't have an account?" : "Already have an account?"}</span>
                    <button
                        className="btn btn-link p-1 text-decoration-none fw-bold"
                        onClick={() => {
                            setIsLogin(!isLogin);
                            setMessage({ text: '', type: '' });
                            setConfirmPassword('');
                        }}
                    >
                        {isLogin ? ' Register Now' : ' Login here'}
                    </button>
                </div>
            </div>
        </div>
    );
};
