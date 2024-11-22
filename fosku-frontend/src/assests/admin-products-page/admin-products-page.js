import "./admin-products-page.css";
import { backend_server_url } from "../../settings";
import { useEffect, useState } from "react";
import AdminNavbarComponent from "../admin-navbar/admin-navbar";
import { useNavigate } from "react-router-dom";

export function AdminPageProductComponent({ product }) {
  const navigate = useNavigate();

  return (
    <div className="product-card">
      <div className="product-image">
        <img
          src={`${backend_server_url}${product.logoUrl}`}
          alt={product.altText}
        />
      </div>
      <div className="product-info">
        <h4>{product.name}</h4>
        <p>Id: {product.id}</p>
        <p>Description: {product.description}</p>
        <p className="price">${product.price.toFixed(2)}</p>
        <p className="stock">In Stock: {product.stockQuantity}</p>
        <button
          type="button"
          className="btn-details"
          onClick={() => navigate(`/admin/products/${product.id}`)}
        >
          View Details
        </button>
      </div>
    </div>
  );
}

export default function AdminProductsPage() {
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

  const [newProduct, setNewProduct] = useState({});
  const [logo, setLogo] = useState();
  const [logoPreviewUrl, setLogoPreviewUrl] = useState("");

  useEffect(() => {
    if (logo) {
      if (logoPreviewUrl) {
        URL.revokeObjectURL(logoPreviewUrl);
      }
      const newPreviewUrl = URL.createObjectURL(logo);
      setLogoPreviewUrl(newPreviewUrl);
      return () => {
        URL.revokeObjectURL(newPreviewUrl);
      };
    }
  }, [logo]);

  const handleImageSelection = (event) => {
    const selectedFile = event.target.files[0];
    setLogo(selectedFile);
  };

  const onChangeHandler = (e) => {
    setNewProduct((prev) => ({
      ...prev,
      [e.target.name]: e.target.value,
    }));
  };

  const onSubmit = async (e) => {
    e.preventDefault();
    const token = localStorage.getItem("jwt");

    const newId = await fetch(`${backend_server_url}/product`, {
      method: "POST",
      headers: {
        "content-type": "application/json",
        "Authorization": `Bearer ${token}`
      },
      body: JSON.stringify(newProduct),
    }).then((res) => res.json());

    const data = new FormData();
    data.append("logoImage", logo);

    await fetch(`${backend_server_url}/image/${newId}`, {
      method: "POST",
      headers: {
        "Authorization": `Bearer ${token}`
      },
      body: data,
    });

    window.location.reload();
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

        <div className="create-product-card">
          <h2>Create New Product</h2>
          <form onSubmit={onSubmit}>
            <label htmlFor="name">Name:</label>
            <input
              name="name"
              type="text"
              value={newProduct.name}
              onChange={onChangeHandler}
              required
            />

            <label htmlFor="description">Description:</label>
            <textarea
              name="description"
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
              required
            />

            <label htmlFor="stockQuantity">Stock Quantity:</label>
            <input
              name="stockQuantity"
              type="number"
              min="0"
              step="1"
              value={newProduct.stockQuantity}
              onChange={onChangeHandler}
              required
            />

            <label htmlFor="logoUrl">Logo (should be 150x150 px):</label>
            {logoPreviewUrl && (
              <img
                src={logoPreviewUrl}
                alt={newProduct.altText}
                className="logo-preview"
              />
            )}
            <input
              type="file"
              accept="image/*"
              onChange={handleImageSelection}
            />
            <label htmlFor="logoAltText">Logo Alt Text</label>
            <input type="text" name="logoAltText" onChange={onChangeHandler} />

            <button type="submit" className="btn-create">
              Save New Product
            </button>
          </form>
        </div>
      </div>
    </div>
  );
}
