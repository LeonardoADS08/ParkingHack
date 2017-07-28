var mongoose = require("mongoose");
var autopopulate = require("mongoose-autopopulate");
var idValidator = require("mongoose-id-validator");
var parkingSpotSchema = new mongoose.Schema({
    longitude: Number,
    latitude:Number,
    nombre: String,
    disponible: Boolean,
    tipo:{
        type: String,
        enum :["publico","privado"]
    },
    costo: Number,
    horario:[[Number]],
    encargado:{
        type: mongoose.Schema.Types.ObjectId,
        ref: "Keeper",
         autopopulate: true
    },
    active: {
        type:Boolean,
        required : true,
        default:true
    }
});
parkingSpotSchema.plugin(idValidator);
parkingSpotSchema.plugin(autopopulate);
mongoose.model("ParkingSpot",parkingSpotSchema);
