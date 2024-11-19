import { Link } from 'react-router-dom';
import './admin-dashboard-page.css'
import AdminNavbarComponent from '../admin-navbar/admin-navbar';

export default function AdminDashboardPage() {
    return (
        <div>
            <AdminNavbarComponent />
            <nav>
                <Link to={"/admin/products"}>Products</Link>
                <Link to={"/admin/orders"}>Orders</Link>
            </nav>
        </div>
    );
}