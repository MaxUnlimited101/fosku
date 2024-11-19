import logo from './logo.svg';
import { BrowserRouter, Routes, Route } from "react-router-dom";
import './App.css';
import MainPage from './assests/main-page/main-page';
import ProductsPage from './assests/products-page/products-page';
import ContactPage from './assests/contact-page/contact-page';
import AboutUsPage from './assests/about-us-page/about-us-page';
import NotFoundPage from './assests/not-found-page/not-found-page';
import AdminPage from './assests/admin-page/admin-page';
import ProductDetailsPage from './assests/product-details-page/product-details-page';
import AdminDashboardPage from './assests/admin-dashboard-page/admin-dashboard-page';
import AdminProductsPage from './assests/admin-products-page/admin-products-page';
import AdminOrdersPage from './assests/admin-orders-page/admin-orders-page';
import AdminProductDetailsPage from './assests/admin-product-details-page/admin-product-details-page';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/">
          <Route index element={<MainPage />} />
          <Route path="products" element={<ProductsPage />} />
          <Route path="products/:id" element={<ProductDetailsPage />} />
          <Route path="contact" element={<ContactPage />} />
          <Route path="about" element={<AboutUsPage />} />

          <Route path="admin" element={<AdminPage />} />
          <Route path="admin/dashboard" element={<AdminDashboardPage />} />
          <Route path="admin/products" element={<AdminProductsPage />} />
          <Route path="admin/products/:id" element={<AdminProductDetailsPage />} />
          <Route path="admin/orders" element={<AdminOrdersPage />} />
          <Route path="admin/products/create" element={<AdminProductsPage />} />

          <Route path="*" element={<NotFoundPage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
