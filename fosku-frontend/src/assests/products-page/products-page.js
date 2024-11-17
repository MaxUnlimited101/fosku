// ProductsPage.js
import React, { useState, useEffect } from "react";
import PageWrapper from "../page-wrapper/page-wrapper";

// Sample product data (Replace this with actual API data)
let sampleProducts = [
  {
    id: 1,
    name: "Product 1",
    price: 29.99,
    image: "https://via.placeholder.com/150",
    description: "This is a great product.",
  },
  {
    id: 2,
    name: "Product 2",
    price: 49.99,
    image: "https://via.placeholder.com/150",
    description: "An amazing product for everyone.",
  },
  {
    id: 3,
    name: "Product 3",
    price: 79.99,
    image: "https://via.placeholder.com/150",
    description: "High quality and affordable.",
  },
  {
    id: 4,
    name: "Product 4",
    price: 99.99,
    image: "https://via.placeholder.com/150",
    description: "Best in its category.",
  },
  {
    id: 5,
    name: "Product 5",
    price: 19.99,
    image: "https://via.placeholder.com/150",
    description: "Budget-friendly but high value.",
  },
  {
    id: 6,
    name: "Product 6",
    price: 111.99,
    image: "https://via.placeholder.com/150",
    description: "desc 6",
  },
];

// Product Item Component
const ProductItem = ({ product }) => (
  <div className="product-item">
    <img src={product.image} alt={product.name} />
    <h3>{product.name}</h3>
    <p>{product.description}</p>
    <span>${product.price.toFixed(2)}</span>
    <button>Add to Cart</button>
  </div>
);

// Product Grid Component
const ProductGrid = ({ products }) => (
  <div className="product-grid">
    {products.map((product) => (
      <ProductItem key={product.id} product={product} />
    ))}
  </div>
);

// Filter and Sort Component
const FilterSort = ({ setSortBy }) => {
  return (
    <div className="filter-sort">
      <label>Sort By: </label>
      <select onChange={(e) => setSortBy(e.target.value)}>
        <option value="default">Default</option>
        <option value="price-low-to-high">Price: Low to High</option>
        <option value="price-high-to-low">Price: High to Low</option>
        <option value="name-asc">Name: A to Z</option>
        <option value="name-desc">Name: Z to A</option>
      </select>
    </div>
  );
};

// Pagination Component
const Pagination = ({ totalPages, currentPage, setCurrentPage }) => (
  <div className="pagination">
    {Array.from({ length: totalPages }, (_, index) => (
      <button
        key={index}
        className={currentPage === index + 1 ? "active" : ""}
        onClick={() => setCurrentPage(index + 1)}
      >
        {index + 1}
      </button>
    ))}
  </div>
);

// Main Products Page Component
const ProductsPage = () => {
  const [products, setProducts] = useState(sampleProducts);
  const [sortBy, setSortBy] = useState("default");
  const [currentPage, setCurrentPage] = useState(1);
  const productsPerPage = 4;

  // Sort products based on selected sort criteria
  useEffect(() => {
    let sortedProducts = [...products];

    switch (sortBy) {
      case "price-low-to-high":
        sortedProducts.sort((a, b) => a.price - b.price);
        break;
      case "price-high-to-low":
        sortedProducts.sort((a, b) => b.price - a.price);
        break;
      case "name-asc":
        sortedProducts.sort((a, b) => a.name.localeCompare(b.name));
        break;
      case "name-desc":
        sortedProducts.sort((a, b) => b.name.localeCompare(a.name));
        break;
      default:
        break;
    }

    setProducts(sortedProducts);
  }, [sortBy]);

  // Pagination logic
  const indexOfLastProduct = currentPage * productsPerPage;
  const indexOfFirstProduct = indexOfLastProduct - productsPerPage;
  const currentProducts = products.slice(
    indexOfFirstProduct,
    indexOfLastProduct
  );
  const totalPages = Math.ceil(products.length / productsPerPage);

  return (
    <PageWrapper>
      <div className="products-page">
        <h1>Our Products</h1>
        <FilterSort setSortBy={setSortBy} />
        <ProductGrid products={currentProducts} />
        <Pagination
          totalPages={totalPages}
          currentPage={currentPage}
          setCurrentPage={setCurrentPage}
        />
      </div>
    </PageWrapper>
  );
};

export default ProductsPage;
