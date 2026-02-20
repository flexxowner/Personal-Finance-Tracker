import React, { useEffect, useState } from "react";
import api from '../api/axios.ts';
import {AccountCard} from '../components/AccountCard';

interface Account {
    id: number;
    name: string;
    type: string;
    balance: number;
}

export const AccountsPage = () => {
    const [accounts, setAccounts] = useState<Account[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    
    const handleDelete = async (id: number) => {
        if (confirm(`Are you sure you want to delete ${id} account?`)) {
            try {
                await api.delete(`/api/accounts/${id}`);
                setAccounts((prev) => prev.filter(account => account.id !== id));
            } catch (err) {
                console.error(err);
            }
        }
    }
    
    useEffect(() => {
        const fetchAccounts = async () => {
            try {
                const response = await api.get('/api/accounts');
                setAccounts(response.data);
            } catch (err) {
                console.error(err);
            } finally {
                setIsLoading(false);
            }
        };
        
        fetchAccounts();
    },[])
    
    if (isLoading) return <div className="p-8">Loading...</div>;
    
    return(
        <div>
            <div className="mb-8 flex justify-between items-center">
                <div>
                    <h2 className="text-2xl font-bold text-gray-900">
                        Your Accounts
                    </h2>
                    <p className="text-gray-500">Manage your financial accounts</p>
                </div>
                {accounts.length > 0 && (
                    <button className="bg-blue-600 text-white px-4 py-2 rounded-lg hover:bg-blue-700 transition">
                        + Add Account
                    </button>
                )}
            </div>

            {accounts.length == 0 ? (
                <div className="mt-8 bg-white p-16 rounded-2xl shadow-sm border border-gray-100 flex flex-col items-center text-center">
                    <div className="w-16 h-16 bg-gray-100 rounded-full flex items-center justify-center mb-4 text-3xl">
                        💳
                    </div>

                    <h3 className="text-xl font-bold text-gray-900 mb-2">
                        No accounts yet
                    </h3>
                    <p className="text-gray-500 mb-6 max-w-sm">
                        Create your first account to start tracking your finances.
                        It could be cash, a bank account, or a credit card.
                    </p>

                    <button className="bg-blue-600 text-white px-6 py-3 rounded-lg font-medium hover:bg-blue-700 transition shadow-lg shadow-blue-600/20">
                        + Create Account
                    </button>
                </div>
            ) : (
                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                    {accounts.map((account) => (
                        <AccountCard key={account.id}
                                     id={account.id}
                                     name={account.name}
                                     type={account.type}
                                     balance={account.balance}
                                     onDelete={handleDelete}/>
                    ))}
                </div>
            )}
        </div>
    )
}