function logout() {
    var tokenKey = 'accessToken';
    sessionStorage.removeItem(tokenKey);

    window.location.href = "";
}

function loadIndexPage() {
    var tokenKey = 'accessToken';
    var pathArray = location.href.split('/');
    var protocol = pathArray[0];
    var host = pathArray[2];
    var url = protocol + '//' + host + '/api/portfolio';

    var tokenKey = 'accessToken';
    var token = sessionStorage.getItem(tokenKey);

    $('#spinner').show();

    setTimeout(function () {

        if (token) {
            $("#divLogin").hide();
            $("#logoutLink").show();
            $("#divPortfolio").show();
            loadPortfolio();
        }
        else {
            $("#divLogin").show();
            $("#logoutLink").hide();
            $("#divPortfolio").hide();
        }
    }, 1000);

    $('#spinner').hide();
}

function loadPortfolio() {
    // Send an AJAX request
    var tokenKey = 'accessToken';
    var pathArray = location.href.split('/');
    var protocol = pathArray[0];
    var host = pathArray[2];
    var url = protocol + '//' + host + '/api/portfolio';
    var token = sessionStorage.getItem(tokenKey);
    var headers = {};
    var securities;

    if (token) {
        headers.Authorization = 'Bearer ' + token;

        $('#spinner').show();
        $.ajax({
            type: 'GET',
            url: url,
            headers: headers
        }).done(function (data) {
            $("span[id='portfolioName']").html(data[0].name + " Portfolio");
            drawPortSecTables(data[0].securities);
            $("span[id='portfolioTotalValue']").html("Total Portfolio Value: " + formatToUSCurrency(calculateTotalPortfolioValue(data[0].securities), 0));
            $('#spinner').hide();
        })
        .fail(function (jqXHR) {
            console.log(jqXHR.status + ': ' + jqXHR.statusText);
        });
    }
}

function loadSecurity() {
    var symbol = "'" + $('#securitySym').val() + "'";
    var url = 'http://query.yahooapis.com/v1/public/yql?q=select * from yahoo.finance.quotes where symbol in (' + symbol +
              ')&env=store://datatables.org/alltableswithkeys&format=json';
    $('#spinner').show();
    $.ajax({
        type: 'GET',
        url: url,
    }).done(function (data) {
        if (data.query.results && data.query.results.quote.Name) {
            drawSecDetailRow(data.query.results.quote);
            $("span[id='symbolError']").html('');
            $("#divSecurity").show();
        }
        else {
            $("span[id='symbolError']").html('Failed to load quote for Symbol: ' + symbol);
        }

        $('#spinner').hide();
    })
    .fail(function (jqXHR) {
        console.log(jqXHR.status + ': ' + jqXHR.statusText);
        $("span[id='symbolError']").html('Failed to Load Symbol:' + symbol.replace("'"));
    });
}

function loadSecurityBySymbol(symbol) {
    var url = 'http://query.yahooapis.com/v1/public/yql?q=select * from yahoo.finance.quotes where symbol in (' + symbol +
              ')&env=store://datatables.org/alltableswithkeys&format=json';
    var quote;
    $.ajax({
        type: 'GET',
        url: url,
        async: false
    }).done(function (data) {
        if (data.query.results && data.query.results.quote.Name) {
            quote = data.query.results.quote;
        }
    })
    .fail(function (jqXHR) {
        console.log(jqXHR.status + ': ' + jqXHR.statusText);
    });

    return quote;
}

function drawPortSecTables(data) {
    $("#stockFundDataTable").find("tr:gt(0)").remove();
    $("#bondDataTable").find("tr:gt(0)").remove();
    for (var i = 0; i < data.length; i++) {
        if (data[i].symbol)
            drawStockFundRow(data[i]);
        else
            drawBondRow(data[i]);
    }
}

function drawStockFundRow(rowData) {
    var row = $("<tr />")
    var quote = loadSecurityBySymbol("'" + rowData.symbol + "'");
    var purchasePrice = 0;
    var lastTradePriceOnly = 0;
    var dividendYield = 0;
    var gain = 0;
    var gainPct = 0;

    $("#stockFundDataTable").append(row);
    if (quote) {

        purchasePrice = rowData.purchasePrice;
        lastTradePriceOnly = quote.LastTradePriceOnly;
        if (quote.DividendYield)
            dividendYield = quote.DividendYield;
        else if (rowData.fundDividend)
            dividendYield = rowData.fundDividend;

        if (rowData.type == "Stock") {
            gain = calculateStockValue(rowData.quantity, lastTradePriceOnly, dividendYield) - (parseFloat(purchasePrice) * parseFloat(rowData.quantity));
            gainPct = gain / (parseFloat(purchasePrice) * parseFloat(rowData.quantity));
        }
        else if (rowData.type == "Fund") {
            gain = calculateFundValue(rowData.quantity, lastTradePriceOnly, dividendYield) - (parseFloat(purchasePrice) * parseFloat(rowData.quantity));
            gainPct = gain / (parseFloat(purchasePrice) * parseFloat(rowData.quantity));
        }

        // Stock / Fund Information
        row.append($("<td>" + rowData.symbol + "</td>")); // Symbol
        row.append($("<td>" + quote.Name + "</td>")); // Name
        row.append($("<td>" + rowData.type + "</td>"));
        row.append($("<td>" + formatToUSCurrency(parseFloat(purchasePrice), 2) + "</td>")); // Purchase Price
        row.append($("<td>" + formatToNumeric(parseInt(rowData.quantity)) + "</td>")); // Quantity
        row.append($("<td>" + formatToUSCurrency((parseFloat(purchasePrice) * parseFloat(rowData.quantity)), 0) + "</td>")); // Cost
        row.append($("<td>" + formatToUSCurrency(parseFloat(lastTradePriceOnly), 2) + "</td>")); // Last Trade Price
        row.append($("<td>" + formatToUSCurrency((parseFloat(lastTradePriceOnly) * parseFloat(rowData.quantity)), 0) + "</td>")); // Market Value
        row.append($("<td>" + formatToUSCurrency(parseFloat(dividendYield), 2) + "</td>")); // Dividend
        row.append($("<td>" + formatToUSCurrency(gain, 0) + "</td>")); // Gain
        row.append($("<td>" + formatToPercent(gainPct, 2) + "</td>")); // Gain %
    }
    else {
        // Stock / Fund Information
        row.append($("<td>" + "N/A" + "</td>")); // Symbol
        row.append($("<td>" + rowData.name + "</td>")); // Name
        row.append($("<td>" + rowData.type + "</td>")); // Type
        row.append($("<td>" + "N/A" + "</td>")); // Purchase Price
        row.append($("<td>" + "N/A" + "</td>")); // Quantity
        row.append($("<td>" + "N/A" + "</td>")); // Cost
        row.append($("<td>" + "N/A" + "</td>")); // Last Trade Price
        row.append($("<td>" + "N/A" + "</td>")); // Market Value
        row.append($("<td>" + "N/A" + "</td>")); // Gain
        row.append($("<td>" + "N/A" + "</td>")); // Gain %
    }
}

function drawBondRow(rowData)
{
    var row = $("<tr />")
    var quote = loadSecurityBySymbol("'" + rowData.symbol + "'");
    $("#bondDataTable").append(row);

    // Bond Information
    row.append($("<td>" + rowData.name + "</td>")); // Name
    row.append($("<td>" + rowData.type + "</td>")); // Type
    row.append($("<td>" + formatToUSCurrency(rowData.faceValue, 0) + "</td>")); // Face Value
    row.append($("<td>" + formatToPercent(rowData.bondInterestRate, 0) + "</td>")); // Coupon Rate
    row.append($("<td>" + formatDateMMDDYYYY(rowData.purchaseDate) + "</td>")); // Purchase Date
    row.append($("<td>" + formatDateMMDDYYYY(rowData.maturityDate) + "</td>")); // Maturity Date
    row.append($("<td>" + rowData.numberOfPeriods + "</td>")); // Years to Maturity
    row.append($("<td>" + formatToPercent(rowData.marketInterestRate, 2) + "</td>")); // Required Return
    row.append($("<td>" + formatToUSCurrency(calculateBondValue(rowData.faceValue, rowData.bondInterestRate, rowData.marketInterestRate, rowData.numberOfPeriods), 0) + "</td>")); // Bond Price
}

function drawSecDetailRow(rowData) {
    var row = $("<tr />")
    $("#securityDataTable").find("tr:gt(0)").remove();
    $("#securityDataTable").append(row);
    row.append($("<td>" + rowData.Name + "</td>"));
    row.append($("<td>" + rowData.Symbol + "</td>"));
    row.append($("<td>" + rowData.StockExchange + "</td>"));
    row.append($("<td>" + formatToUSCurrency(parseFloat(rowData.Open), 2) + "</td>"));
    row.append($("<td>" + formatToUSCurrency(parseFloat(rowData.PreviousClose), 2) + "</td>"));
    row.append($("<td>" + formatToUSCurrency(parseFloat(rowData.LastTradePriceOnly), 2) + "</td>"));
    row.append($("<td>" + formatToUSCurrency(parseFloat(rowData.DividendYield), 2) + "</td>"));
    row.append($("<td>" + formatToUSCurrency(parseFloat(rowData.DaysLow), 2) + "</td>"));
    row.append($("<td>" + formatToUSCurrency(parseFloat(rowData.DaysHigh), 2) + "</td>"));
    row.append($("<td>" + formatToUSCurrency(parseFloat(rowData.YearLow), 2) + "</td>"));
    row.append($("<td>" + formatToUSCurrency(parseFloat(rowData.YearHigh), 2) + "</td>"));
}

function calculateStockValue(quantity, currentPrice, stockDividend) {

    var stockValue = (quantity * currentPrice) + ((stockDividend / currentPrice) * quantity);

    return stockValue;
}

function calculateFundValue(quantity, currentPrice, fundDividend) {

    var fundValue = (quantity * currentPrice) + ((fundDividend / currentPrice) * 4 * quantity);

    return fundValue;
}

function calculateBondValue(faceValue, bondInterestRate, marketInterestRate, numberOfPeriods) {

    var redemptionValue = faceValue / Math.pow(1 + marketInterestRate, numberOfPeriods);
    var interestPaymentValue = bondInterestRate * faceValue * ((1 - Math.pow((1 + marketInterestRate), -numberOfPeriods)) / marketInterestRate);

    return redemptionValue + interestPaymentValue;
}

function calculateTotalPortfolioValue(data) {
    var marketValue = 0;
    var bondPrice = 0;
    for (var i = 0; i < data.length; i++) {
        if (data[i].symbol) {
            marketValue += loadSecurityBySymbol("'" + data[i].symbol + "'").LastTradePriceOnly * data[i].quantity;
        }
        else {
            bondPrice += calculateBondValue(data[i].faceValue, data[i].bondInterestRate, data[i].marketInterestRate, data[i].numberOfPeriods)
        }
    }

    return marketValue + bondPrice;
}

function formatToUSCurrency(value, decimal) {
    return '$' + value.toFixed(decimal).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
}

function formatToNumeric(value, decimal) {
    return value.toFixed(decimal).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
}

function formatToPercent(value, decimal) {
    return (value * 100).toFixed(decimal).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,") + '%'
}

function formatDateMMDDYYYY(dateString) {
    var date = new Date(dateString);
    return (date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear();
}