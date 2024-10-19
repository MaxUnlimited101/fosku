// NotFoundPage.js
import React from 'react';
import { Link } from 'react-router-dom';
import './not-found-page.css'

const NotFoundPage = () => {
  return (
    <div className="not-found-page">
      <h1>404</h1>
      <h2>Oops! Page not found</h2>
      <p>
        The page you are looking for might have been removed, had its name changed, or is temporarily unavailable.
      </p>
      <Link to="/" className="home-link">Go back to Home</Link>
    </div>
  );
};

export default NotFoundPage;
