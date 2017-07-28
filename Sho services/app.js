var express = require("express");
var db = require("./data/db.js");
var routes = require("./routes");
var bodyParser = require('body-parser');
var app = express();

app.use(function (req, res, next) {
    console.log(req.method, req.url);
    next();
});
//use body parser for post requests
app.use(bodyParser.urlencoded({extended : true}));
app.use(bodyParser.json());
//routes 
app.use("/api", routes);
//setting app port
app.set('port',(process.env.PORT || 8080));
var server = app.listen(app.get('port'), function() {
    var port  = server.address().port;
    console.log("magic happening in port : " + port);
});