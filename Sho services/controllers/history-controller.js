var mongoose = require("mongoose");
var History = mongoose.model("History");
var helpers = require("./helpers.js");
var messages = require("../messages.js");

module.exports.addHistory = function(req, res){
    if(!req.body.fecha || !req.body.parkingSpot || !req.body.user){
        helpers.handleError({
            statusCode:400,
            message: messages.E10001 // missing obligatory data
        }, res);
        return;
    }
    var history = {
        fecha : Date.parse(req.body.fecha),
        parkingSpot: req.body.parkingSpot,
        user: req.body.user
    };
    History.create(history).then(history=>{
        res.status(201).json(history);
    }).catch(err=>{
        helpers.handleError(err, res);
    });
}
module.exports.getHistories = function(req, res){
    req.query.active = true;
    History.find(req.query).then(histories=>{
        res.status(200).json(histories);
    }).catch(err=>{
        helpers.handleError(err, res);
    });
}
module.exports.getHistory = function(req, res){
    History.findById(req.params.historyId).then(history=>{
        if(!history){
            throw({
                statusCode:404,
                message:messages.E10002 // not found
            });
        }
        res.status(200).json(history);
    }).catch(err=>{
        helpers.handleError(err, res);
    });
}
module.exports.updateHistory = function(req, res){
    History.findByIdAndUpdate({_id:req.params.historyId},{$set:req.body}).then(history=>{
        if(!history){
            throw({
                statusCode:404,
                message:messages.E10002 // not found
            });
            return;
        }
        res.status(204).json();
    }).catch(err=>{
        helpers.handleError(err, res);
    });
}
module.exports.deleteHistory = function(req, res){
    History.findByIdAndUpdate({_id:req.params.historyId},{$set:{active:false}}).then(history=>{
        if(!history){
            throw({
                statusCode:404,
                message:messages.E10002 // not found
            });
        }
        res.status(204).json();
    }).catch(err=>{
        helpers.handleError(err, res);
    });
}