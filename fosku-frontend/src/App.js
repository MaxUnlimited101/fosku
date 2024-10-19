import logo from './logo.svg';
import { BrowserRouter, Routes, Route } from "react-router-dom";
import './App.css';
import MainPage from './assests/main-page/main-page';
import ProductsPage from './assests/products-page/products-page';
import ContactPage from './assests/contact-page/contact-page';
import AboutUsPage from './assests/about-us-page/about-us-page';
import NotFoundPage from './assests/not-found-page/not-found-page';
import AdminPage from './assests/admin-page/admin-page';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/">
          <Route index element={<MainPage />} />
          <Route path="products" element={<ProductsPage />} />
          <Route path="contact" element={<ContactPage />} />
          <Route path="about" element={<AboutUsPage /> } />
          <Route path="admin" element={<AdminPage /> } />
          <Route path="*" element={<NotFoundPage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
