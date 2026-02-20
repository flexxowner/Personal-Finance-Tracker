import React from 'react';
import { useNavigate } from 'react-router-dom';

interface AccountCardProps {
    id: number,
    name: string,
    type: string,
    balance: number,
    onDelete: (id: number) => void;
}

export const AccountCard = ({ id, name, type, balance, onDelete }: AccountCardProps) => {
    const navigate = useNavigate();
    return (
        <div onClick={() => navigate(`/accounts/${id}`)}
             className="bg-white p-6 rounded-2xl shadow-sm border border-gray-100 hover:shadow-md transition-shadow">
            <div className="flex justify-between items-start md-6">
                <div className="flex items-center gap-4">
                    <div className="w-12 h-12 bg-blue-50 rounded-xl flex items-center justify-center text-blue-600">
                        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
                            <rect width="20" height="14" x="2" y="5" rx="2"/><line x1="2" x2="22" y1="10" y2="10"/>
                        </svg>
                    </div>

                    <div>
                        <h3 className="font-bold text-gray-900 text-lg leading-tight">{name}</h3>
                        <p className="text-sm text-gray-500">{type}</p>
                    </div>
                </div>
                <button 
                    onClick={(e) => {
                        e.stopPropagation();
                        onDelete(id);
                    }}
                    className="text-gray-400 hover:text-red-500 transition-colors p-1"
                    title="Delete account">
                    <svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
                        <path d="M3 6h18"/><path d="M19 6v14c0 1-1 2-2 2H7c-1 0-2-1-2-2V6"/><path d="M8 6V4c0-1 1-2 2-2h4c1 0 2 1 2 2v2"/>
                    </svg>
                </button>
            </div>
            <div className="mb-4">
                <p className="text-xs font-medium text-gray-500 uppercase tracking-wider">
                    Balance
                </p>
                <p className="text-2xl font-bold text-gray-900 mt-1">
                    ${balance.toFixed(2)}
                </p>
            </div>

            <div className="border-t border-gray-50 pt-4 mt-4">
                <p className="text-sm text-gray-500 flex items-center gap-2">
                    Linked Budgets: <span className="font-medium text-gray-900">0</span>
                </p>
            </div>
        </div>
    )
}