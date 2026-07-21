<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AboutUc.ascx.cs" Inherits="User_Controls_AboutUc" %>


<div class="about-section">
  <div class="container">

    <!-- Row 1 -->
    <div class="about-row">
      <div class="about-box">
        <h2 class="word-break text-center">WHAT IS AFMS</h2>
        <p> AFMS (Accounting & Financial Management System) is a modern web-based solution designed to simplify financial record-keeping for businesses. The system provides an efficient way to manage accounts, monitor transactions and view financial performance through automated reports and dashboards.</p>
        <p class="mb-0">AFMS aims to reduce manual workload and ensure accuracy, transparency and timely financial decision-making.</p>
      </div>

      <div class="about-box">
        <h3>How We Are Different?</h3>
        <ul class="unordered-list">
          <li>Real-time financial dashboard with clear charts and analytics.</li>
          <li>100% transparency in recording debit and credit transactions.</li>
          <li>Automated financial reports including Income Statement and Receivables.</li>
          <li>Reliable support and continuous system improvements.</li>
        </ul>
      </div>
    </div>

    <!-- Row 2 -->
    <div class="about-row">
      <div class="about-box">
        <h3>Who We Are?</h3>
        <ul class="unordered-list">
          <li>A dedicated team focused on delivering a reliable financial management solution.</li>
          <li>Our goal is to help businesses maintain accurate accounts with minimal effort.</li>
          <li>We believe in consistency, accuracy, and user satisfaction.</li>
         <li>We use modern technology to ensure smooth and error-free financial operations.</li>
        </ul>
      </div>

      <div class="about-box">
        <h3>What We Do?</h3>
        <ul class="unordered-list">
          <li>Manage General Ledger</li>
          <li>Handle Debit and Credit Transactions</li>
          <li>Generate Income Statements</li>
          <li>Manage Payables & Receivables</li>
          <li>Provide Real-Time Financial Dashboards</li>
          <li>Maintain User Accounts and Activity Logs</li>
        </ul>
      </div>
    </div>

  </div>
</div>

<style>
body {
  margin: 0;
  font-family: "Segoe UI", sans-serif;
  background:
    linear-gradient(rgba(0, 0, 0, 0.25), rgba(0, 0, 0, 0.25)),
    url("../Images/images (2).jfif");
  background-size: cover;
  background-position: center;
  background-attachment: fixed;
}



/* ===== About Section ===== */
.about-section {
  padding: 100px 20px;
}

/* ===== Container ===== */
.container {
  max-width: 1200px;
  margin: auto;
}

/* ===== CARD GRID (no sections now) ===== */
.about-row {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(320px, 1fr));
  gap: 35px;
  margin-bottom: 40px;
  padding: 0;          /* section feel removed */
  background: none;    /* section bg removed */
}

/* ===== CARDS ===== */
.about-box {
  background: rgba(255, 255, 255, 0.08);
  backdrop-filter: blur(14px);
  padding: 35px;
  border-radius: 22px;
  border: 1px solid rgba(255, 255, 255, 0.15);
  position: relative;
  transition: all 0.35s ease;
}

/* Hover Effect */
.about-box:hover {
  transform: translateY(-12px) scale(1.03);
  background: rgba(255, 255, 255, 0.12);
  box-shadow: 0 30px 70px rgba(0, 0, 0, 0.7);
}

/* Accent Line */
.about-box::before {
  content: "";
  position: absolute;
  left: 0;
  top: 0;
  height: 100%;
  width: 5px;
  background: linear-gradient(to bottom, #38bdf8, #6366f1);
  border-radius: 5px 0 0 5px;
}

/* ===== HEADINGS ===== */
.about-box h2,
.about-box h3 {
  color: #f1f5f9;
  margin-bottom: 15px;
  font-weight: 700;
}

.about-box h2 {
  font-size: 28px;
  text-align: center;
}

.about-box h3 {
  font-size: 21px;
}

/* ===== TEXT ===== */
.about-box p {
  color: #c7d2fe;
  font-size: 15.5px;
  line-height: 1.9;
}

/* ===== LISTS ===== */
.unordered-list {
  list-style: none;
  padding: 0;
}

.unordered-list li {
  position: relative;
  padding-left: 26px;
  margin-bottom: 14px;
  color: #e0e7ff;
  font-size: 15px;
}

.unordered-list li::before {
  content: "✦";
  position: absolute;
  left: 0;
  color: #38bdf8;
}

/* ===== RESPONSIVE ===== */
@media (max-width: 768px) {
  .about-section {
    padding: 70px 15px;
  }

  .about-box {
    padding: 28px;
  }
}

</style>

<script src="../App_Themes/js/jquery.min.js"></script>

<script src=" https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"> </script>
<script type="text/javascript" src="../App_Themes/js/Crypto.js"></script>

<!-- JS here -->
    <script src="../App_Themes/js/vendor/modernizr-3.5.0.min.js"></script>
    <!-- <script src="js/vendor/jquery-1.12.4.min.js"></script> -->
    <script src="../App_Themes/js/popper.min.js"></script>
    <script src="../App_Themes/js/bootstrap.min.js"></script>
    <script src="../App_Themes/js/owl.carousel.min.js"></script>
    <script src="../App_Themes/js/isotope.pkgd.min.js"></script>
    <script src="../App_Themes/js/ajax-form.js"></script>
    <script src="../App_Themes/js/waypoints.min.js"></script>
    <script src="../App_Themes/js/jquery.counterup.min.js"></script>
    <script src="../App_Themes/js/imagesloaded.pkgd.min.js"></script>
    <script src="../App_Themes/js/scrollIt.js"></script>
    <script src="../App_Themes/js/jquery.scrollUp.min.js"></script>
    <script src="../App_Themes/js/wow.min.js"></script>
    <script src="../App_Themes/js/nice-select.min.js"></script>
    <script src="../App_Themes/js/jquery.slicknav.min.js"></script>
    <script src="../App_Themes/js/jquery.magnific-popup.min.js"></script>
    <script src="../App_Themes/js/plugins.js"></script>
    <!-- <script src="js/gijgo.min.js"></script> -->
    <script src="../App_Themes/js/slick.min.js"></script>
    <script src="../App_Themes/js/numbertowordconvertconvert.js"></script>

    
    <!--contact js-->
    <script src="../App_Themes/js/contact.js"></script>
    <script src="../App_Themes/js/jquery.ajaxchimp.min.js"></script>
    <script src="../App_Themes/js/jquery.form.js"></script>
  

    <script src="../App_Themes/js/main.js"></script>
  
    <!--toastr-->
   <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/2.0.1/js/toastr.js"></script>
   <script type="text/javascript" src="//cdn.jsdelivr.net/jquery/1/jquery.min.js"></script>
    <script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>
    <script src="http://code.jquery.com/jquery-migrate-1.2.1.min.js"></script>
 <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
