import { Link } from 'react-router-dom';
import './admin-dashboard-page.css'

export default function AdminDashboardPage() {
    return (
        <div>
            <nav>
                <Link to={"/admin/products"}>Products</Link>
                <Link to={"/admin/orders"}>Orders</Link>
            </nav>
        </div>
    );
}