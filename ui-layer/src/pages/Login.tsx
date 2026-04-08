import React, { useState, ChangeEvent, FormEvent } from 'react';
import axios from 'axios';
import type { User } from '../models/User';
import { useNavigate } from 'react-router-dom';

const API_BASE_URL = "https://localhost:7123/api/Users";
export default function Login() {
    const navigate = useNavigate();

    //UI State
    const [isLogin, setIsLogin] = useState<boolean>(true);
    const [message, setMessage] = useState<{ text: string; type: string }>({ text: '', type: '' });

    // Form State
    const [email, setEmail] = useState<string>('');
    const [password, setPassword] = useState<string>('');

    // 1. Handle Login
    const onLogin = async (e: FormEvent) => {
        e.preventDefault();
        try {
            const response = await axios.get<boolean>(`${API_BASE_URL}/validate`, {
                params: { email, password }
            });

            if (response.data) {
                setMessage({ text: "Login Successful!", type: "alert-success" });
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
        try {
            const newUser: User = { email, password, role: "User" };
            await axios.post(API_BASE_URL, newUser);
            setMessage({ text: "Account created! You can now login.", type: "alert-success" });
            setIsLogin(true); // Switch to login form
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

                <form onSubmit={isLogin ? onLogin : onRegister}>
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
                        onClick={() => { setIsLogin(!isLogin); setMessage({ text: '', type: '' }); }}
                    >
                        {isLogin ? ' Register Now' : ' Login here'}
                    </button>
                </div>
            </div>
        </div>
    );
};
