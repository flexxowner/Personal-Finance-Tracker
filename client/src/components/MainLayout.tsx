import React from 'react';
import {Outlet, Link, useNavigate} from 'react-router-dom';

const NavLink = ({ to, children }: { to: string, children: React.ReactNode }) => (
    <Link
        to={to}
        className="text-gray-500 hover:text-indigo-600 px-1 py-2 text-sm font-medium border-b-2 border-transparent hover:border-indigo-600 transition-all"
    >
        {children}
    </Link>
);

export const MainLayout= () => {
    const navigate = useNavigate();
    
    const handleLogout = () => {
        localStorage.removeItem('token');
        navigate('/login');
    }
    
    return (
        <div className="min-h-screen bg-gray-50 text-gray-900 font-sans">
            <header className="bg-white shadow-sm border-b border-gray-200 sticky top-0 z-10">
                <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
                    <div className="flex justify-between h-16 items-center">
                        <div className="flex items-center gap-2">
                            <span className="text-2xl">🚀</span>
                            <h1 className="text-xl font-bold bg-gradient-to-r from-indigo-600 to-blue-500 bg-clip-text text-transparent">
                                Finance Tracker
                            </h1>
                        </div>
                        
                        <nav className="hidden md:flex space-x-8">
                            <NavLink to="/">Dashboard</NavLink>
                            <NavLink to="/accounts">Accounts</NavLink>
                            <NavLink to="/budgets">Budgets</NavLink>
                        </nav>
                        
                        <button onClick={handleLogout}
                                className="text-sm text-gray-500 hover:text-red-600 transition-colors font-medium">
                            Logout
                        </button>
                    </div>
                </div>
            </header>
            
            <main className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
                <Outlet />
            </main>
        </div>
    )
}