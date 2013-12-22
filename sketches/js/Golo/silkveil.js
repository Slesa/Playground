
var mappings = {
    'slesa' : {
        action: 'redirect',
        url: 'http://www.slesa.de',
        type: 'permanent'
    },
    'logo': {
        action: 'download',
        url: 'http://www.goloroden.de/images/logo.png',
        fileName: 'PolarBear.png',
        contentType: 'image/png',
        forceDownload: false
    }
};


var actions = {
    'download': function (res,mapping) {

    },
    'error': function (res, mapping){
        res.writeHead(mapping.statusCode, {
            'Content-Type': 'text/html'
        });
        res.end(mapping.statusCode + ' ' + mapping.data);
    },
    'redirect': function (res, mapping){
        var statusCode = mapping.type=='permanent' ? 301 : 307;
        res.writeHead(statusCode, {
            'Location': mapping.url
        });
        res.end();
    }
};

var http = require('http');
http.createServer( function(req, res) {
   var alias = req.url.substring(1);
    var mapping = mappings[alias] || {
        action: 'error',
        statusCode: 404,
        data: 'File not found'
    };
    actions[mapping.action](res, mapping);
}).listen(3000);


console.log('Up and running...');
