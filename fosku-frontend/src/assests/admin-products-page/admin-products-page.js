import "./admin-products-page.css";
import { backend_server_url } from "../../settings";
import { useState } from "react";
import useSWR from "swr";
import AdminNavbarComponent from "../admin-navbar/admin-navbar";
import { useNavigate } from "react-router-dom";

export function AdminPageProductComponent({ product }) {
  const [productChanged, setProduct] = useState(product);
  const navigate = useNavigate();

  const onChangeHandler = (e) => {
    setProduct((prev) => ({
      ...prev,
      [e.target.name]: e.target.value,
    }));
  };

  const onSubmit = (e) => {
    e.preventDefault();
    if (e.nativeEvent.submitter.name === "btnUpdate") {
      fetch(`${backend_server_url}/product`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(productChanged),
      }).then(_ => alert("Success!")).catch(_ => alert("Error! Something went wrong!"));
    } else if (e.nativeEvent.submitter.name === "btnDelete") {
      if (
        window.confirm(
          "Are you sure you want to DELETE this product? This action is irreversible!"
        )
      ) {
        fetch(`${backend_server_url}/product`, {
          method: "DELETE",
          headers: {
            "Content-Type": "application/json",
          },
          body: `${productChanged.id}`,
        }).then(_ => alert("Success!")).catch(_ => alert("Error! Something went wrong!"));
      } else {
        return;
      }
    }
  };

  return (
    <div className="product-card">
      <form onSubmit={onSubmit}>
        <label htmlFor="id">ID:</label>
        <input
          name="id"
          type="text"
          readOnly
          value={productChanged.id}
          className="input-readonly"
        />

        <label htmlFor="name">Name:</label>
        <input
          name="name"
          type="text"
          value={productChanged.name}
          onChange={onChangeHandler}
        />

        <label htmlFor="description">Description:</label>
        <input
          name="description"
          type="text"
          value={productChanged.description}
          onChange={onChangeHandler}
        />

        <label htmlFor="price">Price:</label>
        <input
          name="price"
          type="number"
          step={0.01}
          value={productChanged.price}
          onChange={onChangeHandler}
        />

        <label htmlFor="stockQuantity">Stock Quantity:</label>
        <input
          name="stockQuantity"
          type="number"
          min="0"
          step="1"
          value={productChanged.stockQuantity}
          onChange={onChangeHandler}
        />

        <button type="submit" name="btnUpdate" className="btn-update">
          Update Product
        </button>
        <button type="submit" name="btnDelete" className="btn-delete">
          Delete Product
        </button>
        <button
          type="button"
          className="btn-details"
          onClick={(_) => navigate(`/admin/products/${product.id}`)}
        >
          View details
        </button>
      </form>
    </div>
  );
}

const fetcher = (url) => fetch(url).then((res) => res.json());

export default function AdminProductsPage() {
  const {
    data: products,
    error,
    isValidating,
  } = useSWR(`${backend_server_url}/products`, fetcher);

  const [newProduct, setNewProduct] = useState({
    name: "",
    description: "",
    price: "",
    stockQuantity: "",
  });

  const onChangeHandler = (e) => {
    setNewProduct((prev) => ({
      ...prev,
      [e.target.name]: e.target.value,
    }));
  };

  const onSubmit = (e) => {
    e.preventDefault();

    fetch(`${backend_server_url}/product`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(newProduct),
    }).then(_ => alert("Success!")).catch(_ => alert("Error! Something went wrong!"));
  };

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

  return (
    <div>
      <AdminNavbarComponent />
      <div className="admin-products-page">
        <h1>Admin Products Page</h1>

        <div className="products-grid">
          {products?.map((product) => (
            <AdminPageProductComponent key={product.id} product={product} />
          ))}
        </div>

        <div className="product-card">
          <h2>Create New Product</h2>
          <form onSubmit={onSubmit}>
            <label htmlFor="name">Name:</label>
            <input
              name="name"
              type="text"
              value={newProduct.name}
              onChange={onChangeHandler}
            />

            <label htmlFor="description">Description:</label>
            <input
              name="description"
              type="text"
              value={newProduct.description}
              onChange={onChangeHandler}
            />

            <label htmlFor="price">Price:</label>
            <input
              name="price"
              type="number"
              step="0.01"
              min={0}
              value={newProduct.price}
              onChange={onChangeHandler}
            />

            <label htmlFor="stockQuantity">Stock Quantity:</label>
            <input
              name="stockQuantity"
              type="number"
              min="0"
              step="1"
              value={newProduct.stockQuantity}
              onChange={onChangeHandler}
            />

            <button type="submit" className="btn-create">
              Save New Product
            </button>
          </form>
        </div>
      </div>
    </div>
  );
}
