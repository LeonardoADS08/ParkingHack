var mongoose = require("mongoose");
var userSchema = new mongoose.Schema({
    idFacebook:String,
    placa:String,
    telefono:String,
    nombre:String,
    active: {
        type:Boolean,
        default:true
    }
});
mongoose.model("User",userSchema);