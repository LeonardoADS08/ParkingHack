module.exports.handleError = function(err, res){
    if(err.statusCode){
        res.status(err.statusCode).json({
            message: err.message
        });
    }
    else{
        res.status(500).json("SERVER INTERNAL ERROR");
        console.log(err);
    }
}