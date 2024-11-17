// MainPage.js
import React from "react";
import "./main-page.css";
import PageWrapper from "../page-wrapper/page-wrapper";
import { useNavigate } from "react-router-dom";

// Components

const HeroSection = () => (
  <section className="hero">
    <div className="hero-content">
      <h2>Welcome to Our Shop</h2>
      <p>Find the best products at unbeatable prices!</p>
      <button>Shop Now</button>
    </div>
  </section>
);

const ProductCategories = () => (
  <section className="categories">
    <h2>Shop by Categories</h2>
    <div className="category-grid">
      <div className="category-item">Electronics</div>
      <div className="category-item">Clothing</div>
      <div className="category-item">Home & Kitchen</div>
      <div className="category-item">Beauty & Health</div>
    </div>
  </section>
);

const FeaturedProducts = () => (
  <section className="featured-products">
    <h2>Featured Products</h2>
    <div className="product-grid">
      <div className="product-item">
        <img src="https://via.placeholder.com/150" alt="Product 1" />
        <h3>Product 1</h3>
        <p>$29.99</p>
      </div>
      <div className="product-item">
        <img src="https://via.placeholder.com/150" alt="Product 2" />
        <h3>Product 2</h3>
        <p>$59.99</p>
      </div>
      <div className="product-item">
        <img src="https://via.placeholder.com/150" alt="Product 3" />
        <h3>Product 3</h3>
        <p>$99.99</p>
      </div>
      <div className="product-item">
        <img src="https://via.placeholder.com/150" alt="Product 4" />
        <h3>Product 4</h3>
        <p>$49.99</p>
      </div>
    </div>
  </section>
);

const MainPage = () => {
  return (
    <PageWrapper>
      <div className="main-page">
        <HeroSection />
        <ProductCategories />
        <FeaturedProducts />
      </div>
    </PageWrapper>
  );
};

export default MainPage;
