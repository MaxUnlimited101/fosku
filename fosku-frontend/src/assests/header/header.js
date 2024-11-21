import { Link } from 'react-router-dom';
import './header.css'

export const Header = () => (
    <header className="header">
      <div className="logo">
        <h1>ShopLogo</h1>
      </div>
      <nav className="navigation">
        <ul>
          <li><Link to="/">Home</Link></li>
          <li><Link to="/products">Products</Link></li>
          <li><Link to="/contact">Contact</Link></li>
          <li><Link to="/about">About Us</Link></li>
        </ul>
      </nav>
    </header>
  );

  export default Header;