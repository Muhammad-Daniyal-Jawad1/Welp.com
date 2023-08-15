<%@ Page Language ="C#" %>

<!DOCTYPE html>

<html>
<head>
    <title>Welp.com - Reviews and Business Listings</title>
    <link rel="stylesheet" type="text/css" href="index.css" />
</head>
<body>
    <form id="form1" runat="server">
        <header>
            <div class="logo">
                <a href="index.aspx">
                     <img src="welplogo.png" alt="welp.com" style="width:60px;height:60px;">
                </a>
            </div>
            <nav>
                <ul>
                    <li><a href="#">Reviews</a></li>
                    <li><a href="search.aspx">Business Listings</a></li>
                    <li><a href="signup.aspx">Sign Up</a></li>
                    <li><a href="Login.aspx">Log In</a></li>
                </ul>
            </nav>
        </header>

        <main>
            <section class="hero">
                <img class="bg-image" src="index.jpg" alt="Background Image" />
                <div class="content">
                    <h1>Find and review great businesses near you!</h1>
                    <p>Read honest reviews from real customers and discover new businesses in your area.</p>
                    <button>Write a Review</button>
                </div>
            </section>


            <section class="reviews">
                <h2>"Recent Reviews"</h2>
                <div class="review">
                    <h3>Great food and atmosphere!</h3>
                    <p>
                        Visited this restaurant with my family last week and we had an amazing time. The food was delicious
                        and the staff was friendly and attentive. Highly recommend!
                    </p>
                    <div class="rating">4.5</div>
                </div>

                <div class="review">
                    <h3>Excellent service and quality products</h3>
                    <p>
                        I have been a loyal customer of this business for years and I am always impressed by their
                        exceptional service and high-quality products. Keep up the good work!
                    </p>
                    <div class="rating">5</div>
                </div>

                <div class="review">
                    <h3>Disappointing experience</h3>
                    <p>
                        Unfortunately, my experience at this business was not a pleasant one. The staff was rude and
                        unhelpful and the product was of poor quality. Would not recommend.
                    </p>
                    <div class="rating">1</div>
                </div>
            </section>

            <section class="business-listings">
                <h2>"Business Listings"</h2>
                <div class="business">
                    <h3>Joe's Pizza</h3>
                    <p>123 Main St, Anytown USA</p>
                    <a href ="#">Visit Website</a>
                </div>

                <div class="business">
                    <h3>ABC Gym</h3>
                    <p>456 Park Ave, Anytown USA</p>
                    <a href ="#">Visit Website</a>
                </div>

                <div class="business">
                    <h3>XYZ Salon</h3>
                    <p>789 Elm St, Anytown USA</p>
                     <a href ="#">Visit Website</a>
                </div>
            </section>
        </main>

        <footer>
            <p>&copy; <%= DateTime.Now.Year %> Welp.com. All rights reserved.</p>
               </footer>
    </form>
</body>
</html>