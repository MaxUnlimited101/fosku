import { useNavigate, useParams } from "react-router-dom";
import "./admin-product-details-page.css";
import { backend_server_url } from "../../settings";
import { useEffect, useState } from "react";
import AdminNavbarComponent from "../admin-navbar/admin-navbar";

export default function AdminProductDetailsPage() {
  const { id } = useParams();
  const [error, setError] = useState(null);
  const [isValidating, setIsValidating] = useState(true);
  const [productChanged, setProduct] = useState();
  const navigate = useNavigate();

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
  }, [id]);

  const onChangeHandler = (e) => {
    setProduct((prev) => ({
      ...prev,
      [e.target.name]: e.target.value,
    }));
  };

  const onSubmit = (e) => {
    e.preventDefault();
    const token = localStorage.getItem("jwt");

    if (e.nativeEvent.submitter.name === "btnUpdate") {
      fetch(`${backend_server_url}/product`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
          "Authorization": `Bearer ${token}`
        },
        body: JSON.stringify(productChanged),
      }).then(_ => window.location.reload());
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
            "Authorization": `Bearer ${token}`
          },
          body: JSON.stringify({ id: productChanged.id }),
        }).then(_ => navigate("/admin/products/"));
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
        <p>Loading... (if loading takes too long, try reloading the page)</p>
      </div>
    );
  }

  return (
    <div>
      <AdminNavbarComponent />
      <div className="admin-product-details-page">
        <h1>Product Details</h1>
        <div className="product-card">
          <form onSubmit={onSubmit}>
            <div className="form-group">
              <label htmlFor="id">ID (not changable):</label>
              <input
                name="id"
                type="text"
                readOnly
                value={productChanged.id}
                className="input-readonly"
              />
            </div>

            <div className="form-group">
              <label htmlFor="name">Name:</label>
              <input
                name="name"
                type="text"
                value={productChanged.name}
                onChange={onChangeHandler}
              />
            </div>

            <div className="form-group">
              <label htmlFor="description">Description:</label>
              <textarea
                name="description"
                value={productChanged.description}
                onChange={onChangeHandler}
              />
            </div>

            <div className="form-group">
              <label htmlFor="price">Price:</label>
              <input
                name="price"
                type="number"
                step="0.01"
                value={productChanged.price}
                onChange={onChangeHandler}
              />
            </div>

            <div className="form-group">
              <label htmlFor="stockQuantity">Stock Quantity:</label>
              <input
                name="stockQuantity"
                type="number"
                min="0"
                step="1"
                value={productChanged.stockQuantity}
                onChange={onChangeHandler}
              />
            </div>

            <div className="form-group">
              <label htmlFor="logoUrl">Logo (150x150 px):</label>
              <div className="image-preview">
                <img
                  src={`${backend_server_url}${productChanged.logoUrl}`}
                  alt={productChanged.altText}
                />
              </div>
            </div>

            <div className="form-group">
              <label htmlFor="logoAltText">Logo Alt Text:</label>
              <input
                type="text"
                name="logoAltText"
                onChange={onChangeHandler}
                value={productChanged.logoAltText}
              />
            </div>

            <div className="form-buttons">
              <button type="submit" name="btnUpdate" className="btn-update">
                Update Product
              </button>
              <button type="submit" name="btnDelete" className="btn-delete">
                Delete Product
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
}
