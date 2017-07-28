var mongoose = require("mongoose");
var keeperSchema = new mongoose.Schema({
    telefono:String,
    nombre:String,
    active: {
        type:Boolean,
        default:true
    }
});
mongoose.model("Keeper",keeperSchema);