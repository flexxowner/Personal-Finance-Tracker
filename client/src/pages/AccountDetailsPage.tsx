import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';

// --- Interfaces ---

interface Budget {
    id: string;
    categoryId: string;
    categoryName: string;
    name: string;
    limit: number;
    startDate: Date;
    endDate: Date;
    spent: number; // Добавил поле для визуализации прогресса (в реальном API это может приходить с бека)
}

interface Transaction {
    id: string;
    accountId: string;
    categoryId: string;
    categoryName: string;
    budgetId?: string;
    type: string; // 'income' | 'expense'
    amount: number;
    exchangeRate: number;
    note: string;
    date: string; // Добавил, так как это нужно для отображения
}

export const AccountDetailsPage = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [loading, setLoading] = useState(true);

    const [account, setAccount] = useState<any>(null);
    const [transactions, setTransactions] = useState<Transaction[]>([]);
    const [budgets, setBudgets] = useState<Budget[]>([]);

    useEffect(() => {
        // Эмуляция загрузки данных
        setTimeout(() => {
            // 1. Данные аккаунта
            setAccount({
                id,
                name: "Main Card",
                balance: 12450.00,
                currency: "USD",
                type: "Card",
                holder: "ROMAN K."
            });

            // 2. Данные бюджетов
            setBudgets([
                {
                    id: 'b1',
                    categoryId: 'c1',
                    categoryName: 'Food & Groceries',
                    name: 'Monthly Food',
                    limit: 500,
                    spent: 350, // Фейковое значение потраченного
                    startDate: new Date('2023-10-01'),
                    endDate: new Date('2023-10-31')
                },
                {
                    id: 'b2',
                    categoryId: 'c2',
                    categoryName: 'Transport',
                    name: 'Gas & Taxi',
                    limit: 200,
                    spent: 45,
                    startDate: new Date('2023-10-01'),
                    endDate: new Date('2023-10-31')
                }
            ]);

            // 3. Данные транзакций
            setTransactions([
                {
                    id: '1',
                    accountId: id || 'acc1',
                    categoryId: 'c1',
                    categoryName: 'Food & Groceries',
                    budgetId: 'b1',
                    type: 'expense',
                    amount: -45.50,
                    exchangeRate: 1,
                    note: 'Grocery Store Purchase',
                    date: 'Today, 10:23 AM'
                },
                {
                    id: '2',
                    accountId: id || 'acc1',
                    categoryId: 'c_inc',
                    categoryName: 'Income',
                    type: 'income',
                    amount: 1200.00,
                    exchangeRate: 1,
                    note: 'Freelance Project Payment',
                    date: 'Yesterday'
                },
                {
                    id: '3',
                    accountId: id || 'acc1',
                    categoryId: 'c3',
                    categoryName: 'Entertainment',
                    type: 'expense',
                    amount: -12.99,
                    exchangeRate: 1,
                    note: 'Netflix Subscription',
                    date: 'Feb 18'
                },
                {
                    id: '4',
                    accountId: id || 'acc1',
                    categoryId: 'c2',
                    categoryName: 'Transport',
                    budgetId: 'b2',
                    type: 'expense',
                    amount: -30.00,
                    exchangeRate: 1,
                    note: 'Shell Gas Station',
                    date: 'Feb 17'
                },
            ]);

            setLoading(false);
        }, 600);
    }, [id]);

    if (loading) return (
        <div className="flex h-[50vh] items-center justify-center">
            <div className="w-10 h-10 border-4 border-indigo-500 border-t-transparent rounded-full animate-spin"></div>
        </div>
    );

    if (!account) return <div>Account not found</div>;

    return (
        <div className="max-w-7xl mx-auto">
            <button
                onClick={() => navigate('/accounts')}
                className="group flex items-center gap-2 text-gray-400 hover:text-gray-900 transition mb-8 font-medium"
            >
                <span className="group-hover:-translate-x-1 transition-transform">←</span> Back to Accounts
            </button>

            <div className="grid grid-cols-1 lg:grid-cols-12 gap-8">

                {/* === ЛЕВАЯ КОЛОНКА (Карта + Статистика) === */}
                <div className="lg:col-span-5 space-y-6">

                    {/* 🔥 ВИЗУАЛЬНАЯ КАРТА */}
                    <div className="relative h-64 rounded-[2rem] bg-gradient-to-br from-slate-900 via-slate-800 to-slate-900 text-white p-8 shadow-2xl shadow-slate-900/20 overflow-hidden transform hover:scale-[1.02] transition-transform duration-300">
                        {/* Декор */}
                        <div className="absolute top-0 right-0 -mr-16 -mt-16 w-64 h-64 rounded-full bg-white/5 blur-3xl"></div>
                        <div className="absolute bottom-0 left-0 -ml-16 -mb-16 w-64 h-64 rounded-full bg-indigo-500/20 blur-3xl"></div>

                        <div className="relative h-full flex flex-col justify-between z-10">
                            <div className="flex justify-between items-start">
                                <div>
                                    <p className="text-slate-400 text-sm font-medium tracking-wider mb-1">Current Balance</p>
                                    <h2 className="text-4xl font-bold tracking-tight">
                                        ${account.balance.toLocaleString('en-US', { minimumFractionDigits: 2 })}
                                    </h2>
                                </div>
                                <div className="w-12 h-12 bg-white/10 backdrop-blur-md rounded-xl flex items-center justify-center border border-white/10">
                                    {account.type === 'Card' ? '💳' : account.type === 'Cash' ? '💵' : '🏦'}
                                </div>
                            </div>

                            <div>
                                <div className="flex gap-4 items-center mb-6">
                                    <div className="w-10 h-8 bg-gradient-to-tr from-yellow-200 to-yellow-500 rounded-md opacity-80"></div>
                                    <div className="text-white/30 text-xl tracking-widest">•••• •••• •••• 4289</div>
                                </div>
                                <div className="flex justify-between items-end">
                                    <div>
                                        <p className="text-slate-400 text-xs uppercase tracking-widest mb-1">Card Holder</p>
                                        <p className="font-medium tracking-wide">{account.holder}</p>
                                    </div>
                                    <div className="text-right">
                                        <p className="text-slate-400 text-xs uppercase tracking-widest mb-1">Expires</p>
                                        <p className="font-medium">12/28</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    {/* Блок быстрой статистики */}
                    <div className="grid grid-cols-2 gap-4">
                        <div className="bg-white p-5 rounded-3xl shadow-[0_8px_30px_rgb(0,0,0,0.04)] border border-gray-100">
                            <div className="w-10 h-10 bg-green-50 text-green-600 rounded-full flex items-center justify-center mb-3 text-lg">↓</div>
                            <p className="text-gray-500 text-sm font-medium">Income</p>
                            <p className="text-xl font-bold text-gray-900">+$2,450.00</p>
                        </div>
                        <div className="bg-white p-5 rounded-3xl shadow-[0_8px_30px_rgb(0,0,0,0.04)] border border-gray-100">
                            <div className="w-10 h-10 bg-red-50 text-red-600 rounded-full flex items-center justify-center mb-3 text-lg">↑</div>
                            <p className="text-gray-500 text-sm font-medium">Expenses</p>
                            <p className="text-xl font-bold text-gray-900">-$840.50</p>
                        </div>
                    </div>

                    <button className="w-full py-4 rounded-2xl border-2 border-dashed border-gray-200 text-gray-400 font-medium hover:border-gray-300 hover:text-gray-600 transition flex items-center justify-center gap-2">
                        ⚙️ Account Settings
                    </button>
                </div>

                {/* === ПРАВАЯ КОЛОНКА (Бюджеты + Транзакции) === */}
                <div className="lg:col-span-7 space-y-8">

                    {/* 1. Блок Бюджетов */}
                    <div className="bg-white rounded-[2.5rem] p-8 shadow-sm border border-gray-100">
                        <div className="flex justify-between items-center mb-6">
                            <h3 className="text-2xl font-bold text-gray-900">Active Budgets</h3>
                            {budgets.length > 0 && <span className="text-sm text-gray-500">{budgets.length} running</span>}
                        </div>

                        <div className="space-y-4">
                            {budgets.length > 0 ? budgets.map(budget => {
                                const percent = Math.min((budget.spent / budget.limit) * 100, 100);
                                return (
                                    <div key={budget.id} className="p-4 bg-gray-50 rounded-2xl border border-gray-100">
                                        <div className="flex justify-between items-center mb-2">
                                            <div>
                                                <h4 className="font-bold text-gray-900">{budget.name}</h4>
                                                <p className="text-xs text-gray-500">{budget.categoryName}</p>
                                            </div>
                                            <div className="text-right">
                                                <span className="font-bold text-gray-900">${budget.spent}</span>
                                                <span className="text-gray-400 text-sm"> / ${budget.limit}</span>
                                            </div>
                                        </div>
                                        {/* Прогресс бар */}
                                        <div className="w-full bg-gray-200 rounded-full h-2.5">
                                            <div
                                                className={`h-2.5 rounded-full ${percent > 90 ? 'bg-red-500' : 'bg-blue-600'}`}
                                                style={{ width: `${percent}%` }}
                                            ></div>
                                        </div>
                                    </div>
                                )
                            }) : (
                                <p className="text-gray-400 text-center">No active budgets linked to this account.</p>
                            )}
                        </div>
                    </div>

                    {/* 2. Блок Транзакций */}
                    <div className="bg-white rounded-[2.5rem] p-8 shadow-sm border border-gray-100">
                        <div className="flex justify-between items-center mb-8">
                            <h3 className="text-2xl font-bold text-gray-900">Transactions</h3>
                            <button className="text-indigo-600 font-medium hover:text-indigo-700 text-sm">View All</button>
                        </div>

                        <div className="space-y-4">
                            {transactions.length > 0 ? (
                                transactions.map((tx) => (
                                    <div
                                        key={tx.id}
                                        className="group flex items-center justify-between p-4 hover:bg-gray-50 rounded-2xl transition-colors cursor-pointer"
                                    >
                                        <div className="flex items-center gap-4">
                                            {/* Иконка */}
                                            <div className={`w-12 h-12 rounded-2xl flex items-center justify-center text-xl transition-transform group-hover:scale-110 ${
                                                tx.type === 'income' ? 'bg-green-100 text-green-600' : 'bg-gray-100 text-gray-600'
                                            }`}>
                                                {tx.type === 'income' ? '💰' : '🛒'}
                                            </div>
                                            <div>
                                                {/* Используем note как заголовок */}
                                                <h4 className="font-bold text-gray-900">{tx.note}</h4>
                                                {/* Категория + Дата */}
                                                <p className="text-sm text-gray-500">{tx.categoryName} • {tx.date}</p>
                                            </div>
                                        </div>
                                        <div className={`text-lg font-bold ${
                                            tx.type === 'income' ? 'text-green-600' : 'text-gray-900'
                                        }`}>
                                            {tx.type === 'income' ? '+' : ''}
                                            ${Math.abs(tx.amount).toFixed(2)}
                                        </div>
                                    </div>
                                ))
                            ) : (
                                <div className="text-center py-10 text-gray-400">
                                    No transactions yet
                                </div>
                            )}
                        </div>
                    </div>
                </div>

            </div>
        </div>
    );
}