var mongoose = require("mongoose");
var Spot = mongoose.model("ParkingSpot");
var helpers = require("./helpers.js");
var messages = require("../messages.js");

module.exports.addSpot = function(req, res){
    var obligatoryFields = ["latitude","longitude","nombre","tipo","encargado"];
    var spot = {};
    obligatoryFields.map(field=>{
        if(!req.body || !req.body[field]){
            helpers.handleError({
                statusCode:400,
                message: messages.E10001 // missing obligatory data
            }, res);
            return;
        }
        spot[field] = req.body[field];
    });
    spot.disponibilidad = req.body.disponible || false;
    spot.costo = req.body.costo || -1;
    spot.horario = req.body.horario || [];
    Spot.create(spot).then(spot=>{
        res.status(201).json(spot);
    }).catch(err=>{
        helpers.handleError(err,res)
    });
}
module.exports.getSpots = function(req, res){
    req.query.active = true;
    Spot.find(req.query).then(spots=>{
        res.status(200).json(spots);
    }).catch(err=>{
        helpers.handleError(err,res)
    });
}
module.exports.getSpot = function(req, res){
    Spot.findById(req.params.spotId).then(spot=>{
        if(!spot){
            throw({
                statusCode:404,
                message:messages.E10002 // not found
            });
        }
        res.status(200).json(spot);
    }).catch(err=>{
        helpers.handleError(err,res)
    });
}
module.exports.updateSpot = function(req, res){
    Spot.findByIdAndUpdate({_id:req.params.spotId},{$set:req.body}).then(spot=>{
        res.status(204).json();
    }).catch(err=>{
        helpers.handleError(err,res)
    });
}
module.exports.deleteSpot = function(req, res){
    Spot.findByIdAndUpdate({_id:req.params.spotId},{$set:{active:false}}).then(spot=>{
        res.status(204).json();
    }).catch(err=>{
        helpers.handleError(err,res)
    });
}