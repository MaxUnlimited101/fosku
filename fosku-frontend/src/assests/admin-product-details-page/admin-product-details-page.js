import { useParams } from "react-router-dom";
import "./admin-product-details-page.css";
import useSWR from "swr";
import { backend_server_url } from "../../settings";
import { useEffect, useState } from "react";
import AdminNavbarComponent from "../admin-navbar/admin-navbar";

export default function AdminProductDetailsPage() {
  const { id } = useParams();
  const fetcher = (url) => fetch(url).then((res) => res.json());
  const {
    data: product,
    error,
    isValidating,
  } = useSWR(`${backend_server_url}/product/${id}`, fetcher);
  const [productChanged, setProduct] = useState(product);
  useEffect(() => {
    setProduct(product);
  }, [product]);

  if (error) {
    return (
      <div className="error-message">
        <h2>Server is down!</h2>
      </div>
    );
  }

  if (isValidating || productChanged === undefined || productChanged === null) {
    return (
      <div className="loading-message">
        <p>Loading... (if loading is too long, try reloading the page)</p>
      </div>
    );
  }

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
      })
        .then((_) => alert("Success!"))
        .catch((_) => alert("Error! Something went wrong!"));
    }
  };

  return (
    <div>
      <AdminNavbarComponent />
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
        </form>
      </div>
    </div>
  );
}
