import Footer from "../footer/footer";
import Header from "../header/header";
import "./page-wrapper.css";

export default function PageWrapper(props) {
  return (
    <div className="page-wrapper">
      <Header />
      <div className="main">{props.children}</div>
      <Footer />
    </div>
  );
}
