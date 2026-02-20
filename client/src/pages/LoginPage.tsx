import React, {useState} from 'react';
import api from '../api/axios.ts';

export const LoginPage = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    
    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setError('');
        try {
            const response = await api.post('/api/auth/login', {
                email: email,
                password: password
            });
            
            const token = response.data;
            console.log("Token:", token);
            alert("Loged in! Token in the console.");
        }
        catch (err: any) {
            console.error(err);
            if (err.response) {
                setError(err.response.data);
            } else{
                setError("Network Error");
            }
        }
    };
    
    return (
        <div className="min-h-screen flex items-center justify-center bg-gray-900 px-4">
            <div className="max-w-md w-full bg-gray-800 rounded-xl shadow-lg p-8 border border-gray-700">
                <div className="text-center mb-8">
                    <h2 className="text-3xl font-bold text-white">
                        Login
                    </h2>
                    <p className="mt-2 text-sm text-gray-400">
                        Finance Tracker
                    </p>
                </div>
                <form className="space-y-6" onSubmit={handleSubmit}>
                    <div>
                        <label htmlFor="email" className="block text-sm font-medium text-gray-300">
                            Email
                        </label>
                        <div className="mt-1">
                            <input id="email"
                                   name="email"
                                   type="email"
                                   required
                                   className="w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-lg text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-transparent transition"
                                   placeholder="name@example.com"
                                   value={email}
                                   onChange={(e) => setEmail(e.target.value)}/>
                        </div>
                    </div>
                    
                    <div>
                        <label htmlFor="password" className="block text-sm font-medium text-gray-300">
                            Password
                        </label>
                        <div className="mt-1">
                            <input id="password"
                                   name="password"
                                   type="password"
                                   required
                                   className="w-full px-4 py-2 bg-gray-700 border border-gray-600 rounded-lg text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-transparent transition"
                                   placeholder="Your password"
                                   value={password}
                                   onChange={(e) => setPassword(e.target.value)}/>
                        </div>
                    </div>
                    <div>
                        <button type="submit"
                                className="w-full flex justify-center py-2 px-4 border border-transparent rounded-lg shadow-sm text-sm font-medium text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500 transition-colors">
                            Log in
                        </button>
                    </div>
                </form>
            </div>
        </div>
    )
}