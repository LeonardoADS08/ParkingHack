var mongoose = require('mongoose');
var dbUrl = "mongodb://sholopolis:sho123@ds127163.mlab.com:27163/heroku_rf0dq4d3";
var connection = mongoose.connect(dbUrl);


mongoose.connection.on("connected", function() {
    console.log("mongoose connected to " + dbUrl);
});

mongoose.connection.on("disconnected", function() {
    console.log("mongoose disconnected ");
});

mongoose.connection.on("error", function(error) {
    console.log("mongoose connection error " + error);
});

process.on("SIGINT", function() {
    mongoose.connection.close(function() {
        console.log("ctr C pressed");
        process.exit(0);
    });
});

require("./models/user-model.js");
require("./models/keeper-model.js");
require("./models/parkingSpot-model.js");
require("./models/history-model.js");