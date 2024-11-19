import React from "react";
import { Link } from "react-router-dom";
import "./admin-navbar.css";

export default function AdminNavbarComponent() {
  const handleSignOut = () => {
    // TODO: Logic for signing out (e.g., clearing tokens, redirecting)
    console.log("User signed out");
  };

  return (
    <nav className="navbar">
      <div className="navbar-logo">
        <Link to="/">MyShop</Link>
      </div>
      <div className="navbar-links">
        <Link to="/" className="navbar-button">
          Home
        </Link>
        <Link to={-1} className="navbar-button">
          Go back
        </Link>
        <button onClick={handleSignOut} className="navbar-button signout-btn">
          Sign Out
        </button>
      </div>
    </nav>
  );
}
