# Portfolio Viewer
Simple Portfolio Viewer that allows users to see their portfolios and get quotes for Stocks/Bonds

#### Setup
Sample Users / Portfolios are created through Seed method

Please see ApplicationDbInitializer (Users) / PortfolioViewerContextInitializer (Portfolios) classes for credentials / portfolio examples

#### Usage
User logs into tool and sees their Portfolio and Securities within that Portfolio and also have the ability to look up quotes by searching Stock/Fund symbols

#### API Information
[API Help Page] (http://localhost:51754/Help). This was not exposed on the interface.

Quote API Used: [http://query.yahooapis.com/v1/public/yql?q=select * from yahoo.finance.quotes where symbol in ('exampleQuote')&env=store://datatables.org/alltableswithkeys&format=json]
