<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContactUc.ascx.cs" Inherits="User_Controls_ContactUc" %>
        
       

<style>
.text-primary,
.btn-primary,
.contact-info__icon i {
    color: #6b7280 !important;
}
.btn-primary {
    background-color: #6b7280 !important;
    border-color: #6b7280 !important;
    color: #ffffff !important;
}
.btn-primary:hover {
    background-color: #4b5563 !important;
    border-color: #4b5563 !important;
    color: #ffffff !important;
}
.contact-box {
    background: rgba(0,0,0,0.7);
    padding: 20px 25px;
    border-radius: 10px;
    color: #fff;
    margin-bottom: 20px;
    box-shadow: 0 0 15px rgba(0,0,0,0.6);
    display: inline-block;
    width: auto;
}
.contact-info {
    text-align: center;
    margin-bottom: 20px;
}
.contact-info__icon i {
    font-size: 28px;
    margin-bottom: 10px;
    transition: transform 0.3s ease, color 0.3s ease;
}
.contact-info__icon i:hover {
    transform: scale(1.2);
    color: #0dcaf0;
}
.contact-info h3 {
    font-size: 18px;
    font-weight: bold;
    margin-bottom: 5px;
}
.contact-info p {
    font-size: 14px;
    color: #ddd;
    margin: 0;
}
.contact-box {
    max-width: 100%;
    overflow: hidden;
}
.contact-info p,
.contact-info h3 {
    word-wrap: break-word;
    overflow-wrap: break-word;
    white-space: normal;
}
.contact-section {
    margin-top: 100px;
}
.contact-box h3,
.contact-box p,
.contact-box label {
    color: #fff !important;
}
.contact-box .form-control {
    background: rgba(255,255,255,0.1);
    border: 1px solid #ccc;
    color: #fff;
}
.contact-box .form-control::placeholder {
    color: #ddd;
}
.contact-title {
    color: #fff;
    font-weight: bold;
}
.contact-info h3 {
    font-size: 1.25rem;
    font-weight: bold;
    margin-bottom: 5px;
}
.phone-number {
    font-size: 1.25rem;
    font-weight: normal;
}
.phone-wrapper {
    width: 100%;
    display: flex;
    justify-content: center;
    align-items: center;
    flex-direction: column;
    position: relative;
    animation: float 4s ease-in-out infinite;
}
.phone {
    width: 300px;
    height: 560px;
    background: linear-gradient(145deg, #0c0c0c, #2a2a2a);
    border-radius: 38px;
    padding: 12px;
    box-shadow:
        0 35px 80px rgba(0,0,0,0.75),
        inset 0 0 10px rgba(255,255,255,0.08);
    position: relative;
    border: 2px solid #6b7280;
    transform: rotate(-6deg);
    transition: 0.4s ease;
}
.phone:hover {
    transform: rotate(-2deg) scale(1.02);
}
.speaker {
    width: 55px;
    height: 5px;
    background: linear-gradient(to right, #555, #111);
    border-radius: 10px;
    margin: 6px auto 10px;
}
.screen {
    height: calc(100% - 30px);
    border-radius: 28px;
    padding: 16px;
    box-sizing: border-box;
    background: rgba(255,255,255,0.18);
    backdrop-filter: blur(12px);
    -webkit-backdrop-filter: blur(12px);
    border: 1px solid rgba(255,255,255,0.3);
    box-shadow: inset 0 0 20px rgba(255,255,255,0.15);
    overflow-y: auto;
}
.screen h2 {
    text-align: center;
    margin-bottom: 14px;
    font-size: 18px;
    color: #000;
    font-weight: bold;
}
.screen input,
.screen textarea {
    width: 100%;
    padding: 9px;
    margin-bottom: 10px;
    border-radius: 10px;
    border: none;
    font-size: 13px;
    background: rgba(255,255,255,0.85);
    box-shadow: inset 0 0 6px rgba(0,0,0,0.1);
}
.screen input:focus,
.screen textarea:focus {
    outline: none;
    background: #fff;
}
.screen button {
    width: 100%;
    padding: 10px;
    background: linear-gradient(135deg, #000, #333);
    color: #fff;
    border: none;
    border-radius: 12px;
    font-size: 14px;
    cursor: pointer;
    box-shadow: 0 8px 20px rgba(0,0,0,0.45);
    transition: 0.3s;
}
.screen button:hover {
    background: linear-gradient(135deg, #222, #000);
    transform: translateY(-2px);
}
#lblStatus {
    display: block;
    margin-top: 8px;
    font-size: 13px;
    text-align: center;
}
</style>

<section class="contact-section py-5">
    <div class="container">
        <div class="row">

            <!-- FORM BOX -->
            <div class="col-lg-6">
                <div class="phone-wrapper">
                    <div class="phone">
                        <div class="speaker"></div>
                        <div class="screen">
                            <h2 class="contact-title text-white mb-4 text-center">Get in Touch</h2>

                            <div class="row form-row">
                                <div class="col-12">
                                    <div class="form-group">

                                        <textarea runat="server" class="form-control w-100" name="message"
                                            id="inp_message" cols="30" rows="4"
                                            placeholder="Enter Message"></textarea>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="form-group">
                                        <input runat="server" class="form-control" name="name"
                                            id="inp_name" type="text" placeholder="Enter your name" />
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="form-group">
                                        <input runat="server" class="form-control" name="email"
                                            id="inp_email" type="email" placeholder="Email" />
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="form-group">
                                        <input runat="server" class="form-control" name="phoneno"
                                            id="inp_phone" type="number" maxlength="11"
                                            placeholder="Enter Phone no" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group mb-0 text-center">
                                <!-- Button directly calls server-side method -->
                                <asp:Button ID="btnSend" runat="server" Text="Send"
                                    CssClass="btn btn-primary btn-lg"
                                    OnClick="SendContactEmail" />
                            </div>

                            <!-- Status message shown after send -->
                            <span runat="server" id="lblStatus"></span>

                        </div>
                    </div>
                </div>
            </div>

            <!-- CONTACT INFO BOX -->
            <div class="col-lg-6">
                <div class="contact-box">
                    <div class="row">

                        <!-- Location -->
                        <div class="col-sm-6">
                            <div class="contact-info text-center">
                                <span class="contact-info__icon d-block mb-2">
                                    <i class="fas fa-map-marker-alt text-primary fa-2x"></i>
                                </span>
                                <h3>Head Office</h3>
                                <p>Suite # 608, 6th Floor Business Plaza, Mumtaz Hassan Road, Karachi, Pakistan</p>
                            </div>
                        </div>

                        <!-- Email -->
                        <div class="col-sm-6">
                            <div class="contact-info text-center">
                                <span class="contact-info__icon d-block mb-2">
                                    <i class="fas fa-envelope-open-text text-primary fa-2x"></i>
                                </span>
                                <h3>saweraarshad9921@gmail.com</h3>
                                <p>Send us your query anytime!</p>
                            </div>
                        </div>

                        <!-- Phone -->
                        <div class="col-12 d-flex justify-content-center align-items-center" style="min-height: 100px;">
                            <div class="contact-info text-center">
                                <span class="contact-info__icon d-block mb-2">
                                    <i class="fas fa-phone-alt text-primary fa-2x"></i>
                                </span>
                                <h3 class="phone-number">0335-9990470</h3>
                                <p>Mon to Sat 11am to 7pm</p>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

        </div>
    </div>
</section>
