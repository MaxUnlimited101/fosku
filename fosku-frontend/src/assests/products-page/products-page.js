import React, { useState, useEffect } from "react";
import PageWrapper from "../page-wrapper/page-wrapper";
import { useNavigate } from "react-router-dom";
import "./products-page.css";
import { backend_server_url } from "../../settings";

const ProductItem = ({ product }) => {
  const navigate = useNavigate();
  return (
    <div className="product-card">

      <h4>{product.name}</h4>
      <p>{product.description}</p>

      <label htmlFor="price">Price:</label>
      <input
        className="input-readonly"
        readOnly
        name="price"
        type="number"
        step={0.01}
        value={product.price}
      />

      <label htmlFor="stockQuantity">Stock Quantity:</label>
      <input
        className="input-readonly"
        readOnly
        name="stockQuantity"
        type="number"
        min="0"
        step="1"
        value={product.stockQuantity}
      />

      <img
        src={`${backend_server_url}${product.logoUrl}`}
        alt={product.altText}
        style={{ width: "150px", height: "150px" }}
      />

      <button
        type="button"
        className="btn-details"
        onClick={(_) => navigate(`/admin/products/${product.id}`)}
      >
        View details
      </button>
    </div>
  );
};

const ProductGrid = ({ products }) => (
  <div className="product-grid">
    {products.map((product) => (
      <ProductItem key={product.id} product={product} />
    ))}
  </div>
);

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

const ProductsPage = () => {
  const [error, setError] = useState(null);
  const [products, setProducts] = useState([]);
  const [isValidating, setIsValidating] = useState(true);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(`${backend_server_url}/products`);
        if (!response.ok) {
          setError("Failed to fetch!");
          return;
        }
        const result = await response.json();
        setProducts(result);
      } catch (error) {
        setError(error.message);
      } finally {
        setIsValidating(false);
      }
    };

    fetchData();
  }, []);

  const [sortBy, setSortBy] = useState("default");
  const [currentPage, setCurrentPage] = useState(1);
  const productsPerPage = 4;

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

  if (error) {
    return (
      <div className="error-message">
        <h3>Server is down!</h3>
      </div>
    );
  }

  if (isValidating) {
    return (
      <div className="loading-message">
        <p>Loading... (if loading is too long, try reloading the page)</p>
      </div>
    );
  }

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
