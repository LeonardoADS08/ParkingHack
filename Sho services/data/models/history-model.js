var mongoose = require("mongoose");
var idValidator = require("mongoose-id-validator");
var autopopulate = require("mongoose-autopopulate");
var historySchema = new mongoose.Schema({
    fecha : Date,
    user:{
        type: mongoose.Schema.Types.ObjectId,
        ref: "User",
        autopopulate: true
    },
    parkingSpot:{
        type: mongoose.Schema.Types.ObjectId,
        ref: "ParkingSpot",
        autopopulate: true
    },
    active: {
        type:Boolean,
        default:true
    }
});
historySchema.plugin(idValidator);
historySchema.plugin(autopopulate);
mongoose.model("History",historySchema,"histories");

