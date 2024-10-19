import './header.css'

export const Header = () => (
    <header className="header">
      <div className="logo">
        <h1>ShopLogo</h1>
      </div>
      <nav className="navigation">
        <ul>
          <li><a href="#home">Home</a></li>
          <li><a href="#products">Products</a></li>
          <li><a href="#contact">Contact</a></li>
          <li><a href="#about">About Us</a></li>
        </ul>
      </nav>
      <div className="search-bar">
        <input type="text" placeholder="Search products..." />
        <button>Search</button>
      </div>
    </header>
  );

  export default Header;