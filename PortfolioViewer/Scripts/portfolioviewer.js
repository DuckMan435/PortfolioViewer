function logout() {
    var tokenKey = 'accessToken';
    sessionStorage.removeItem(tokenKey);

    window.location.href = "";
}

function loadIndexPage() {
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
    var url = protocol + '//' + host + '/api/portfolios';
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
            $("span[id='portfolioTotalValue']").html("Total Portfolio Value: " + formatToUSCurrency(data[0].portfolioValue, 0));
            $('#spinner').hide();
        })
        .fail(function (jqXHR) {
            console.log(jqXHR.status + ': ' + jqXHR.statusText + ' - ' + jqXHR.responseJSON.message);
            $("#divError").fadeIn("slow");
            $("span[id='error']").html("Portfolio API Error: " + "Status (" + jqXHR.status + " " + jqXHR.statusText + ") - " + jqXHR.responseJSON.message);
            $('#spinner').hide();
            $("#divError").fadeOut(5000);
        });
    }
}

function loadSecurity() {
    var symbol = "'" + $('#securitySym').val() + "'";
    var url = 'http://query.yahooapis.com/v1/public/yql?q=select * from yahoo.finance.quotes where symbol in (' + symbol +
              ')&env=store://datatables.org/alltableswithkeys&format=json';
    $('#spinner').show();
    $("#btnQuote").prop('disabled', true);
    $.ajax({
        type: 'GET',
        url: url,
    }).done(function (data) {
        if (data.query.results && data.query.results.quote.Name) {
            drawSecDetailRow(data.query.results.quote);
            $("span[id='symbolError']").html('');
            $("#divSecurity").show();
            $("#btnQuote").prop('disabled', false);
        }
        else {
            $("#divSymbolError").fadeIn();
            $("span[id='symbolError']").html('Failed to load quote for Symbol: ' + symbol);
            $("#divSymbolError").fadeOut(5000);
            $("#btnQuote").prop('disabled', false);
        }

        $('#spinner').hide();
    })
    .fail(function (jqXHR) {
        $("#divSymbolError").fadeIn();
        console.log(jqXHR.status + ': ' + jqXHR.statusText);
        $("span[id='symbolError']").html('Failed to Load Symbol:' + symbol.replace("'"));
        $("#divSymbolError").fadeOut(5000);
        $("#btnQuote").prop('disabled', false);
    });
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
    $("#stockFundDataTable").append(row);

    // Stock / Fund Information
    row.append($("<td>" + rowData.symbol + "</td>")); // Symbol
    row.append($("<td>" + rowData.name + "</td>")); // Name
    row.append($("<td>" + rowData.securityType + "</td>"));
    row.append($("<td>" + formatToUSCurrency(rowData.purchasePrice, 2) + "</td>")); // Purchase Price
    row.append($("<td>" + formatToNumeric(rowData.quantity) + "</td>")); // Quantity
    row.append($("<td>" + formatToUSCurrency(rowData.cost, 0) + "</td>")); // Cost
    row.append($("<td>" + formatToUSCurrency(rowData.currentPrice, 2) + "</td>")); // Last Trade Price
    row.append($("<td>" + formatToUSCurrency(rowData.marketValue, 0) + "</td>")); // Market Value
    row.append($("<td>" + formatToUSCurrency(rowData.dividend, 2) + "</td>")); // Dividend
    row.append($("<td>" + formatToUSCurrency(rowData.currentValue, 0) + "</td>")); // Current Value
    row.append($("<td>" + formatToUSCurrency(rowData.gain, 0) + "</td>")); // Gain
    row.append($("<td>" + formatToPercent(rowData.percentageGain, 2) + "</td>")); // Gain %
}

function drawBondRow(rowData)
{
    var row = $("<tr />")
    $("#bondDataTable").append(row);

    // Bond Information
    row.append($("<td>" + rowData.name + "</td>")); // Name
    row.append($("<td>" + rowData.securityType + "</td>")); // Type
    row.append($("<td>" + formatToUSCurrency(rowData.faceValue, 0) + "</td>")); // Face Value
    row.append($("<td>" + formatToPercent(rowData.bondInterestRate, 0) + "</td>")); // Coupon Rate
    row.append($("<td>" + formatDateMMDDYYYY(rowData.purchaseDate) + "</td>")); // Purchase Date
    row.append($("<td>" + formatDateMMDDYYYY(rowData.maturityDate) + "</td>")); // Maturity Date
    row.append($("<td>" + rowData.numberOfPeriods + "</td>")); // Years to Maturity
    row.append($("<td>" + formatToPercent(rowData.marketInterestRate, 2) + "</td>")); // Required Return
    row.append($("<td>" + formatToUSCurrency(rowData.currentBondPrice, 0) + "</td>")); // Bond Price
}

function drawSecDetailRow(rowData) {
    var row = $("<tr />")
    $("#securityDataTable").find("tr:gt(0)").remove();
    $("#securityDataTable").append(row);

    // Stock / Fund Quote Information
    row.append($("<td>" + rowData.Name + "</td>"));
    row.append($("<td>" + rowData.Symbol + "</td>"));
    row.append($("<td>" + rowData.StockExchange + "</td>"));
    row.append($("<td>" + formatToUSCurrency(rowData.Open, 2) + "</td>"));
    row.append($("<td>" + formatToUSCurrency(rowData.PreviousClose, 2) + "</td>"));
    row.append($("<td>" + formatToUSCurrency(rowData.LastTradePriceOnly, 2) + "</td>"));
    row.append($("<td>" + formatToUSCurrency(rowData.DividendYield, 2) + "</td>"));
    row.append($("<td>" + formatToUSCurrency(rowData.DaysLow, 2) + "</td>"));
    row.append($("<td>" + formatToUSCurrency(rowData.DaysHigh, 2) + "</td>"));
    row.append($("<td>" + formatToUSCurrency(rowData.YearLow, 2) + "</td>"));
    row.append($("<td>" + formatToUSCurrency(rowData.YearHigh, 2) + "</td>"));
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