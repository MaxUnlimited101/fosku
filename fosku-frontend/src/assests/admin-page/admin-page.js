import "./admin-page.css";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import AdminNavbarComponent from "../admin-navbar/admin-navbar";
import { backend_server_url } from "../../settings";

export default function AdminPage() {
  const navigate = useNavigate();
  const [login, setLogin] = useState({});

  const submitHandler = async (e) => {
    e.preventDefault();

    try {
      const response = await fetch(`${backend_server_url}/manager/login`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(login),
      });

      if (!response.ok) {
        throw new Error("Login failed");
      }

      const { token } = await response.json();
      localStorage.setItem("jwt", token);
      navigate("/admin/dashboard");
    } catch (err) {
      //TODO: do smth here
    }
  };

  const onChangeHandler = (e) => {
    setLogin((prev) => ({ ...prev, [e.target.name]: e.target.value }));
  };

  return (
    <div>
      <AdminNavbarComponent />
      <div className="login-container">
        <form onSubmit={submitHandler} className="login-form">
          <h2 className="login-form__title">Log in to the CMS</h2>
          <div className="form-group">
            <input
              name="email"
              type="text"
              placeholder="Email..."
              value={login.email || ""}
              onChange={onChangeHandler}
              className="form-input"
              required
            />
          </div>
          <div className="form-group">
            <input
              name="password"
              type="password"
              placeholder="Password..."
              value={login.password || ""}
              onChange={onChangeHandler}
              className="form-input"
              required
            />
          </div>
          <button type="submit" className="login-form__button">
            Log in
          </button>
        </form>
      </div>
    </div>
  );
}
