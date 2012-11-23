var Swiftype = window.Swiftype || {};
Swiftype.root_url = Swiftype.root_url || "//api.swiftype.com";
if (typeof Swiftype.renderStyle === 'undefined') {
  Swiftype.renderStyle = 'inline';
}


Swiftype.loadScript = function(url, callback) {
  var script = document.createElement('script');
  script.type = 'text/javascript';
  script.async = true;
  script.src = url;

  var entry = document.getElementsByTagName('script')[0];
  entry.parentNode.insertBefore(script, entry);

  if (script.addEventListener) {
    script.addEventListener('load', callback, false);
  } else {
    script.attachEvent('onreadystatechange', function() {
      if (/complete|loaded/.test(script.readyState))
        callback();
    });
  }
};

Swiftype.loadStylesheet = function(url) {
  var link = document.createElement('link');
  link.rel = 'stylesheet';
  link.type = 'text/css';
  link.href = url;
  (document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(link);
};

Swiftype.loadSupportingFiles = function(callback) {
  if (Swiftype.renderStyle === false) {
    Swiftype.loadScript("//swiftype-assets.a.ssl.fastly.net/assets/swiftype_no_render-40bcbcf0f4812e8824d9976889249b66.js", callback);
    Swiftype.loadStylesheet("//swiftype-assets.a.ssl.fastly.net/assets/swiftype-58710b1546a4d6dd62d1d62eb190c201.css");
  } else if (Swiftype.renderStyle === 'overlay' || typeof Swiftype.resultContainingElement === 'undefined') {
    Swiftype.loadScript("//swiftype-assets.a.ssl.fastly.net/assets/swiftype_overlay-81fa4ad4e443483a9de00f794d3b2596.js", callback);
    Swiftype.loadStylesheet("//swiftype-assets.a.ssl.fastly.net/assets/swiftype_overlay-30c8a0ade477a78214db33c4d45b4a7c.css");
  } else if (Swiftype.renderStyle === 'new_page') {
    Swiftype.loadScript("//swiftype-assets.a.ssl.fastly.net/assets/swiftype_newpage-de96a2cd0ce7b7006bd2969da783b5db.js", callback);
    Swiftype.loadStylesheet("//swiftype-assets.a.ssl.fastly.net/assets/swiftype-58710b1546a4d6dd62d1d62eb190c201.css");
  } else if (Swiftype.renderStyle === 'beta') {
    Swiftype.loadScript("//swiftype-assets.a.ssl.fastly.net/assets/swiftype_beta-b664a8e5dfbd4c1a0e7e6ec326966587.js", callback);
    Swiftype.loadStylesheet("//swiftype-assets.a.ssl.fastly.net/assets/swiftype-58710b1546a4d6dd62d1d62eb190c201.css");
  } else if (Swiftype.renderStyle === 'showtime') {
    Swiftype.loadScript("//swiftype-assets.a.ssl.fastly.net/assets/swiftype_showtime-5655b07cd811279bd5d3c64dcd3784a2.js", callback);
    Swiftype.loadStylesheet("//swiftype-assets.a.ssl.fastly.net/assets/swiftype-58710b1546a4d6dd62d1d62eb190c201.css");
  } else {
    Swiftype.loadScript("//swiftype-assets.a.ssl.fastly.net/assets/swiftype_onpage-bac8274c55cfa510674f2b8002300153.js", callback);
    Swiftype.loadStylesheet("//swiftype-assets.a.ssl.fastly.net/assets/swiftype-58710b1546a4d6dd62d1d62eb190c201.css");
  }
};

var Swiftype = (function(window, undefined) {
   Swiftype.loadSupportingFiles(function(){});
   return Swiftype;
})(window);
