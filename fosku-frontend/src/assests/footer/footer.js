import { Link } from 'react-router-dom';
import './footer.css'

export const Footer = () => (
    <footer className="footer">
      <p>&copy; 2024 Your E-commerce Store. All rights reserved.</p>
      <div className="social-links">
        <a href="#facebook">Facebook</a>
        <a href="#instagram">Instagram</a>
        <a href="#X">X</a>
        <Link to="/admin">For admins</Link>
      </div>
    </footer>
  );
  
  export default Footer;