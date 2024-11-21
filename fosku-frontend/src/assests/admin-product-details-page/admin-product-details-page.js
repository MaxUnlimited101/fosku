import { useParams } from "react-router-dom";
import "./admin-product-details-page.css";
import { backend_server_url } from "../../settings";
import { useEffect, useState } from "react";
import AdminNavbarComponent from "../admin-navbar/admin-navbar";

export default function AdminProductDetailsPage() {
  const { id } = useParams();
  const [error, setError] = useState(null);
  const [isValidating, setIsValidating] = useState(true);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(`${backend_server_url}/product/${id}`);
        if (!response.ok) {
          setError("Failed to fetch!");
          return;
        }
        const res = await response.json();
        setProduct(res);
      } catch (error) {
        setError(error.message);
      } finally {
        setIsValidating(false);
      }
    };

    fetchData();
  }, []); // executes this once

  const [productChanged, setProduct] = useState();

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
        .catch((_) => alert("Error! Something went wrong!"))
        .then((_) => alert("Success!"));
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
        })
          .catch((_) => alert("Error! Something went wrong!"))
          .then((_) => alert("Success!"));
      } else {
        return;
      }
    }
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

          <label htmlFor="logoUrl">Logo (should be 150x150 px):</label>
          <img
            src={`${backend_server_url}${productChanged.logoUrl}`}
            alt={productChanged.altText}
            style={{ width: "150px", height: "150px" }}
          />
          <label htmlFor="logoAltText">Logo alt text</label>
          <input
            type="text"
            name="logoAltText"
            onChange={onChangeHandler}
            value={productChanged.logoAltText}
          />

          <button type="submit" name="btnUpdate" className="btn-update">
            Update Product
          </button>
          <button type="submit" name="btnDelete" className="btn-delete">
            Delete Product
          </button>
        </form>
      </div>
    </div>
  );
}
