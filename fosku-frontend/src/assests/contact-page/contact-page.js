// ContactsPage.js
import React, { useState } from "react";
import "./contacts-page.css";
import PageWrapper from "../page-wrapper/page-wrapper";

// Contact Form Component
const ContactForm = () => {
  const [formData, setFormData] = useState({
    name: "",
    email: "",
    subject: "",
    message: "",
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log("Form data submitted:", formData);
    // Reset form
    setFormData({
      name: "",
      email: "",
      subject: "",
      message: "",
    });
  };

  return (
    <form className="contact-form" onSubmit={handleSubmit}>
      <div className="form-group">
        <label>Name</label>
        <input
          type="text"
          name="name"
          value={formData.name}
          onChange={handleChange}
          required
        />
      </div>
      <div className="form-group">
        <label>Email</label>
        <input
          type="email"
          name="email"
          value={formData.email}
          onChange={handleChange}
          required
        />
      </div>
      <div className="form-group">
        <label>Subject</label>
        <input
          type="text"
          name="subject"
          value={formData.subject}
          onChange={handleChange}
        />
      </div>
      <div className="form-group">
        <label>Message</label>
        <textarea
          name="message"
          value={formData.message}
          onChange={handleChange}
          required
        />
      </div>
      <button type="submit">Send Message</button>
    </form>
  );
};

// Contact Information Component
const ContactInfo = () => (
  <div className="contact-info">
    <h3>Contact Information</h3>
    <ul>
      <li>
        <strong>Address:</strong> 123 Business Street, Suite 100, City, Country
      </li>
      <li>
        <strong>Phone:</strong> +123-456-7890
      </li>
      <li>
        <strong>Email:</strong> info@business.com
      </li>
      <li>
        <strong>Hours:</strong> Mon - Fri: 9:00am - 6:00pm
      </li>
    </ul>
  </div>
);

// Map or Social Links (Optional)
const SocialLinks = () => (
  <div className="social-links">
    <h3>Follow Us</h3>
    <ul>
      <li>
        <a href="#facebook">Facebook</a>
      </li>
      <li>
        <a href="#twitter">Twitter</a>
      </li>
      <li>
        <a href="#instagram">Instagram</a>
      </li>
      <li>
        <a href="#linkedin">LinkedIn</a>
      </li>
    </ul>
  </div>
);

// Contacts Page Component
const ContactPage = () => {
  return (
    <PageWrapper>
      <div className="contacts-page">
        <header>
          <h1>Contact Us</h1>
        </header>
        <section className="contact-section">
          <div className="contact-form-wrapper">
            <h2>Get in Touch</h2>
            <ContactForm />
          </div>
          <div className="contact-info-wrapper">
            <ContactInfo />
            <SocialLinks />
          </div>
        </section>
      </div>
    </PageWrapper>
  );
};

export default ContactPage;
