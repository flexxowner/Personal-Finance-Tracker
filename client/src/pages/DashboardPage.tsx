import { useEffect, useState } from 'react';
import api from '../api/axios.ts';

interface UserProfile {
    id: number;
    username: string;
    email: string;
    currency: string;
    totalBalance: number;
}

export const DashboardPage = () => {
    const [user, setUser] = useState<UserProfile | null>(null);
    const [loading, setLoading] = useState(true);
    
    useEffect(() => {
        const fetchProfile = async () => {
            try {
                const response = await api.get('/api/account/me');
                setUser(response.data);
            } catch (error) {
                console.error("Не удалось загрузить профиль", error);
            } finally {
                setLoading(false);
            }
        };

        fetchProfile();
    }, []);

    if (loading) return <div className="text-center mt-10">Loading...</div>;

    return (
        <div>
            <div className="mb-8">
                <h2 className="text-2xl font-bold text-gray-900">
                    Hello, {user?.username}! 👋
                </h2>
                <p className="text-gray-500">Here is an overview of your finances.</p>
            </div>
            
            <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
                
                <div className="bg-white p-6 rounded-2xl shadow-sm border border-gray-100 hover:shadow-md transition-shadow">
                    <div className="flex justify-between items-start">
                        <div>
                            <p className="text-sm font-medium text-gray-500">Overall balance</p>
                            <h3 className="text-3xl font-bold text-gray-900 mt-2">
                                ${user?.totalBalance.toFixed(2)}
                            </h3>
                        </div>
                        <div className="p-3 bg-green-100 text-green-600 rounded-xl">
                            💲
                        </div>
                    </div>
                </div>

                <div className="bg-white p-6 rounded-2xl shadow-sm border border-gray-100 hover:shadow-md transition-shadow">
                    <div className="flex justify-between items-start">
                        <div>
                            <p className="text-sm font-medium text-gray-500">Total Accounts</p>
                            <h3 className="text-3xl font-bold text-gray-900 mt-2">0</h3>
                        </div>
                        <div className="p-3 bg-blue-100 text-blue-600 rounded-xl">
                            🗃️
                        </div>
                    </div>
                </div>
                
                <div className="bg-white p-6 rounded-2xl shadow-sm border border-gray-100 hover:shadow-md transition-shadow">
                    <div className="flex justify-between items-start">
                        <div>
                            <p className="text-sm font-medium text-gray-500">Active Budgets</p>
                            <h3 className="text-3xl font-bold text-gray-900 mt-2">0</h3>
                        </div>
                        <div className="p-3 bg-blue-100 text-blue-600 rounded-xl">
                            💳
                        </div>
                    </div>
                </div>
                
                <div className="bg-white p-6 rounded-2xl shadow-sm border border-gray-100 hover:shadow-md transition-shadow">
                    <div className="flex justify-between items-start">
                        <div>
                            <p className="text-sm font-medium text-gray-500">Monthly expenses</p>
                            <h3 className="text-3xl font-bold text-gray-900 mt-2">$0.00</h3>
                        </div>
                        <div className="p-3 bg-red-100 text-red-600 rounded-xl">
                            📉
                        </div>
                    </div>
                </div>

            </div>
            
            <div className="mt-8 bg-white p-8 rounded-2xl shadow-sm border border-gray-100">
                <h3 className="text-lg font-bold text-gray-900 mb-4">Expense schedule</h3>
                <div className="h-64 bg-gray-50 rounded-xl flex items-center justify-center text-gray-400">
                    There will be a beautiful graph here...
                </div>
            </div>
        </div>
    );
};