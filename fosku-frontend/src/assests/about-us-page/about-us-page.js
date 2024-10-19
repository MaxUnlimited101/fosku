// AboutUsPage.js
import React from "react";
import "./about-us-page.css";
import PageWrapper from "../page-wrapper/page-wrapper";

// Company Overview Component
const CompanyOverview = () => (
  <div className="company-overview">
    <h2>Our Story</h2>
    <p>
      Founded in 2010, [Your Company] started with a simple vision: to provide
      the best quality products and services to our customers. Over the years,
      we have grown into a trusted brand serving thousands of happy clients
      worldwide.
    </p>
    <p>
      We believe in delivering excellence in everything we do, and our
      commitment to quality has been the cornerstone of our success. Today,
      [Your Company] continues to innovate and expand while staying true to its
      core values.
    </p>
  </div>
);

// Mission and Values Component
const MissionValues = () => (
  <div className="mission-values">
    <h2>Our Mission & Values</h2>
    <p>
      Our mission is to empower our customers by providing innovative solutions
      that enhance their lives. We are driven by our core values:
    </p>
    <ul>
      <li>
        <strong>Customer First:</strong> We prioritize the needs and
        satisfaction of our customers above all else.
      </li>
      <li>
        <strong>Innovation:</strong> We constantly strive to improve and offer
        cutting-edge products and services.
      </li>
      <li>
        <strong>Integrity:</strong> We conduct business with honesty and
        transparency.
      </li>
      <li>
        <strong>Community:</strong> We believe in giving back and supporting the
        communities we serve.
      </li>
    </ul>
  </div>
);

// Team Section Component
const Team = () => {
  const teamMembers = [
    {
      id: 1,
      name: "Alice Johnson",
      role: "CEO",
      image: "https://via.placeholder.com/150",
    },
    {
      id: 2,
      name: "Bob Smith",
      role: "CTO",
      image: "https://via.placeholder.com/150",
    },
    {
      id: 3,
      name: "Carla Wright",
      role: "Marketing Lead",
      image: "https://via.placeholder.com/150",
    },
    {
      id: 4,
      name: "David Lee",
      role: "Head of Sales",
      image: "https://via.placeholder.com/150",
    },
  ];

  return (
    <div className="team-section">
      <h2>Meet Our Team</h2>
      <div className="team-grid">
        {teamMembers.map((member) => (
          <div key={member.id} className="team-member">
            <img src={member.image} alt={member.name} />
            <h3>{member.name}</h3>
            <p>{member.role}</p>
          </div>
        ))}
      </div>
    </div>
  );
};

// About Us Page Component
const AboutUsPage = () => {
  return (
    <PageWrapper>
      <div className="about-us-page">
        <header>
          <h1>About Us</h1>
        </header>
        <section className="about-us-content">
          <CompanyOverview />
          <MissionValues />
          <Team />
        </section>
      </div>
    </PageWrapper>
  );
};

export default AboutUsPage;
