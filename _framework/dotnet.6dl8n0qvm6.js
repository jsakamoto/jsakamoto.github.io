//! Licensed to the .NET Foundation under one or more agreements.
//! The .NET Foundation licenses this file to you under the MIT license.

const e=async()=>WebAssembly.validate(new Uint8Array([0,97,115,109,1,0,0,0,1,4,1,96,0,0,3,2,1,0,10,8,1,6,0,6,64,25,11,11])),t=async()=>WebAssembly.validate(new Uint8Array([0,97,115,109,1,0,0,0,1,5,1,96,0,1,123,3,2,1,0,10,15,1,13,0,65,1,253,15,65,2,253,15,253,128,2,11])),o=async()=>WebAssembly.validate(new Uint8Array([0,97,115,109,1,0,0,0,1,5,1,96,0,1,123,3,2,1,0,10,10,1,8,0,65,0,253,15,253,98,11])),n=Symbol.for("wasm promise_control");function i(e,t){let o=null;const i=new Promise(function(n,i){o={isDone:!1,promise:null,resolve:t=>{o.isDone||(o.isDone=!0,n(t),e&&e())},reject:e=>{o.isDone||(o.isDone=!0,i(e),t&&t())}}});o.promise=i;const r=i;return r[n]=o,{promise:r,promise_control:o}}function r(e){return e[n]}function s(e){e&&function(e){return void 0!==e[n]}(e)||Fe(!1,"Promise is not controllable")}const a="__mono_message__",l=["debug","log","trace","warn","info","error"],c="MONO_WASM: ";let d,u,f,m,g,p;function h(e){m=e}function b(e){if(Oe.diagnosticTracing){const t="function"==typeof e?e():e;console.debug(c+t)}}function w(e,...t){console.info(c+e,...t)}function y(e,...t){console.info(e,...t)}function v(e,...t){console.warn(c+e,...t)}function _(e,...t){if(t&&t.length>0&&t[0]&&"object"==typeof t[0]){if(t[0].silent)return;if(t[0].toString)return void console.error(c+e,t[0].toString())}console.error(c+e,...t)}function E(e,t,o){return function(...n){try{let i=n[0];if(void 0===i)i="undefined";else if(null===i)i="null";else if("function"==typeof i)i=i.toString();else if("string"!=typeof i)try{i=JSON.stringify(i)}catch(e){i=i.toString()}t(o?JSON.stringify({method:e,payload:i,arguments:n.slice(1)}):[e+i,...n.slice(1)])}catch(e){f.error(`proxyConsole failed: ${e}`)}}}function R(e,t,o){u=t,m=e,f={...t};const n=`${o}/console`.replace("https://","wss://").replace("http://","ws://");d=new WebSocket(n),d.addEventListener("error",x),d.addEventListener("close",A),function(){for(const e of l)u[e]=E(`console.${e}`,T,!0)}()}function j(e){let t=30;const o=()=>{d?0==d.bufferedAmount||0==t?(e&&y(e),function(){for(const e of l)u[e]=E(`console.${e}`,f.log,!1)}(),d.removeEventListener("error",x),d.removeEventListener("close",A),d.close(1e3,e),d=void 0):(t--,globalThis.setTimeout(o,100)):e&&f&&f.log(e)};o()}function T(e){d&&d.readyState===WebSocket.OPEN?d.send(e):f.log(e)}function x(e){f.error(`[${m}] proxy console websocket error: ${e}`,e)}function A(e){f.debug(`[${m}] proxy console websocket closed: ${e}`,e)}function S(){Oe.preferredIcuAsset=D(Oe.config);let e="invariant"==Oe.config.globalizationMode;if(!e)if(Oe.preferredIcuAsset)Oe.diagnosticTracing&&b("ICU data archive(s) available, disabling invariant mode");else{if("custom"===Oe.config.globalizationMode||"all"===Oe.config.globalizationMode||"sharded"===Oe.config.globalizationMode){const e="invariant globalization mode is inactive and no ICU data archives are available";throw _(`ERROR: ${e}`),new Error(e)}Oe.diagnosticTracing&&b("ICU data archive(s) not available, using invariant globalization mode"),e=!0,Oe.preferredIcuAsset=null}const t="DOTNET_SYSTEM_GLOBALIZATION_INVARIANT",o=Oe.config.environmentVariables;if(void 0===o[t]&&e&&(o[t]="1"),void 0===o.TZ)try{const e=Intl.DateTimeFormat().resolvedOptions().timeZone||null;e&&(o.TZ=e)}catch(e){w("failed to detect timezone, will fallback to UTC")}}function D(e){var t;if((null===(t=e.resources)||void 0===t?void 0:t.icu)&&"invariant"!=e.globalizationMode){const t=e.applicationCulture||(Me?globalThis.navigator&&globalThis.navigator.languages&&globalThis.navigator.languages[0]:Intl.DateTimeFormat().resolvedOptions().locale);e.applicationCulture||(e.applicationCulture=t);const o=e.resources.icu;let n=null;if("custom"===e.globalizationMode){if(o.length>=1)return o[0].name}else t&&"all"!==e.globalizationMode?"sharded"===e.globalizationMode&&(n=function(e){const t=e.split("-")[0];return"en"===t||["fr","fr-FR","it","it-IT","de","de-DE","es","es-ES"].includes(e)?"icudt_EFIGS.dat":["zh","ko","ja"].includes(t)?"icudt_CJK.dat":"icudt_no_CJK.dat"}(t)):n="icudt.dat";if(n)for(let e=0;e<o.length;e++){const t=o[e];if(t.virtualPath===n)return t.name}}return e.globalizationMode="invariant",null}(new Date).valueOf();const k=class{constructor(e){this.url=e}toString(){return this.url}};async function M(e,t){try{const o="function"==typeof globalThis.fetch;if(Ae){const n=e.startsWith("file://");if(!n&&o)return globalThis.fetch(e,t||{credentials:"same-origin"});g||(p=await import(/*! webpackIgnore: true */"url"),g=await import(/*! webpackIgnore: true */"fs")),n&&(e=p.fileURLToPath(e));const i=await g.promises.readFile(e);return{ok:!0,headers:{length:0,get:()=>null},url:e,arrayBuffer:()=>i,json:()=>JSON.parse(i),text:()=>{throw new Error("NotImplementedException")}}}if(o)return globalThis.fetch(e,t||{credentials:"same-origin"});if("function"==typeof read)return{ok:!0,url:e,headers:{length:0,get:()=>null},arrayBuffer:()=>new Uint8Array(read(e,"binary")),json:()=>JSON.parse(read(e,"utf8")),text:()=>read(e,"utf8")}}catch(t){return{ok:!1,url:e,status:500,headers:{length:0,get:()=>null},statusText:"ERR28: "+t,arrayBuffer:()=>{throw t},json:()=>{throw t},text:()=>{throw t}}}throw new Error("No fetch implementation available")}function C(e){return"string"!=typeof e&&Fe(!1,"url must be a string"),!P(e)&&0!==e.indexOf("./")&&0!==e.indexOf("../")&&globalThis.URL&&globalThis.document&&globalThis.document.baseURI&&(e=new URL(e,globalThis.document.baseURI).toString()),e}const I=/^[a-zA-Z][a-zA-Z\d+\-.]*?:\/\//,O=/[a-zA-Z]:[\\/]/;function P(e){return Ae||Ce?e.startsWith("/")||e.startsWith("\\")||-1!==e.indexOf("///")||O.test(e):I.test(e)}let U,L=0;const N=[],$=[],z=new Map,W={"js-module-threads":!0,"js-module-runtime":!0,"js-module-dotnet":!0,"js-module-native":!0,"js-module-diagnostics":!0},F={...W,"js-module-library-initializer":!0},B={...W,dotnetwasm:!0,heap:!0,manifest:!0},V={...F,manifest:!0},H={...F,dotnetwasm:!0},J={dotnetwasm:!0,symbols:!0},q={...F,dotnetwasm:!0,symbols:!0},Q={symbols:!0};function G(e){return!("icu"==e.behavior&&e.name!=Oe.preferredIcuAsset)}function Z(e,t,o){null!=t||(t=[]),Fe(1==t.length,`Expect to have one ${o} asset in resources`);const n=t[0];return n.behavior=o,K(n),e.push(n),n}function K(e){B[e.behavior]&&z.set(e.behavior,e)}function X(e){Fe(B[e],`Unknown single asset behavior ${e}`);const t=z.get(e);if(t&&!t.resolvedUrl)if(t.resolvedUrl=Oe.locateFile(t.name),W[t.behavior]){const e=me(t);e?("string"!=typeof e&&Fe(!1,"loadBootResource response for 'dotnetjs' type should be a URL string"),t.resolvedUrl=e):t.resolvedUrl=le(t.resolvedUrl,t.behavior)}else if("dotnetwasm"!==t.behavior)throw new Error(`Unknown single asset behavior ${e}`);return t}function Y(e){const t=X(e);return Fe(t,`Single asset for ${e} not found`),t}let ee=!1;async function te(){if(!ee){ee=!0,Oe.diagnosticTracing&&b("mono_download_assets");try{const e=[],t=[],o=(e,t)=>{!q[e.behavior]&&G(e)&&Oe.expected_instantiated_assets_count++,!H[e.behavior]&&G(e)&&(Oe.expected_downloaded_assets_count++,t.push(re(e)))};for(const t of N)o(t,e);for(const e of $)o(e,t);Oe.allDownloadsQueued.promise_control.resolve(),Promise.all([...e,...t]).then(()=>{Oe.allDownloadsFinished.promise_control.resolve()}).catch(e=>{throw Oe.err("Error in mono_download_assets: "+e),Ke(1,e),e}),await Oe.runtimeModuleLoaded.promise;const n=async e=>{const t=await e;if(t.buffer){if(!q[t.behavior]){t.buffer&&"object"==typeof t.buffer||Fe(!1,"asset buffer must be array-like or buffer-like or promise of these"),"string"!=typeof t.resolvedUrl&&Fe(!1,"resolvedUrl must be string");const e=t.resolvedUrl,o=await t.buffer,n=new Uint8Array(o);ge(t),await Ie.beforeOnRuntimeInitialized.promise,await Ie.afterInstantiateWasm.promise,Ie.instantiate_asset(t,e,n)}}else J[t.behavior]?("symbols"===t.behavior&&(await Ie.instantiate_symbols_asset(t),ge(t)),J[t.behavior]&&++Oe.actual_downloaded_assets_count):(t.isOptional||Fe(!1,"Expected asset to have the downloaded buffer"),!H[t.behavior]&&G(t)&&Oe.expected_downloaded_assets_count--,!q[t.behavior]&&G(t)&&Oe.expected_instantiated_assets_count--)},i=[],r=[];for(const t of e)i.push(n(t));for(const e of t)r.push(n(e));Promise.all(i).then(()=>{ke||Ie.coreAssetsInMemory.promise_control.resolve()}).catch(e=>{throw Oe.err("Error in mono_download_assets: "+e),Ke(1,e),e}),Promise.all(r).then(async()=>{ke||(await Ie.coreAssetsInMemory.promise,Ie.allAssetsInMemory.promise_control.resolve())}).catch(e=>{throw Oe.err("Error in mono_download_assets: "+e),Ke(1,e),e})}catch(e){throw Oe.err("Error in mono_download_assets: "+e),e}}}let oe=!1;function ne(){if(oe)return;oe=!0;const e=Oe.config,t=[];if(e.assets)for(const t of e.assets)"object"!=typeof t&&Fe(!1,`asset must be object, it was ${typeof t} : ${t}`),"string"!=typeof t.behavior&&Fe(!1,"asset behavior must be known string"),"string"!=typeof t.name&&Fe(!1,"asset name must be string"),t.resolvedUrl&&"string"!=typeof t.resolvedUrl&&Fe(!1,"asset resolvedUrl could be string"),t.hash&&"string"!=typeof t.hash&&Fe(!1,"asset resolvedUrl could be string"),t.pendingDownload&&"object"!=typeof t.pendingDownload&&Fe(!1,"asset pendingDownload could be object"),t.isCore?N.push(t):$.push(t),K(t);else if(e.resources){const o=e.resources;o.wasmNative||Fe(!1,"resources.wasmNative must be defined"),o.jsModuleNative||Fe(!1,"resources.jsModuleNative must be defined"),o.jsModuleRuntime||Fe(!1,"resources.jsModuleRuntime must be defined"),Z($,o.wasmNative,"dotnetwasm"),Z(t,o.jsModuleNative,"js-module-native"),Z(t,o.jsModuleRuntime,"js-module-runtime"),o.jsModuleDiagnostics&&Z(t,o.jsModuleDiagnostics,"js-module-diagnostics");const n=(e,t,o)=>{const n=e;n.behavior=t,o?(n.isCore=!0,N.push(n)):$.push(n)};if(o.coreAssembly)for(let e=0;e<o.coreAssembly.length;e++)n(o.coreAssembly[e],"assembly",!0);if(o.assembly)for(let e=0;e<o.assembly.length;e++)n(o.assembly[e],"assembly",!o.coreAssembly);if(0!=e.debugLevel&&Oe.isDebuggingSupported()){if(o.corePdb)for(let e=0;e<o.corePdb.length;e++)n(o.corePdb[e],"pdb",!0);if(o.pdb)for(let e=0;e<o.pdb.length;e++)n(o.pdb[e],"pdb",!o.corePdb)}if(e.loadAllSatelliteResources&&o.satelliteResources)for(const e in o.satelliteResources)for(let t=0;t<o.satelliteResources[e].length;t++){const i=o.satelliteResources[e][t];i.culture=e,n(i,"resource",!o.coreAssembly)}if(o.coreVfs)for(let e=0;e<o.coreVfs.length;e++)n(o.coreVfs[e],"vfs",!0);if(o.vfs)for(let e=0;e<o.vfs.length;e++)n(o.vfs[e],"vfs",!o.coreVfs);const i=D(e);if(i&&o.icu)for(let e=0;e<o.icu.length;e++){const t=o.icu[e];t.name===i&&n(t,"icu",!1)}if(o.wasmSymbols)for(let e=0;e<o.wasmSymbols.length;e++)n(o.wasmSymbols[e],"symbols",!1)}if(e.appsettings)for(let t=0;t<e.appsettings.length;t++){const o=e.appsettings[t],n=pe(o);"appsettings.json"!==n&&n!==`appsettings.${e.applicationEnvironment}.json`||$.push({name:o,behavior:"vfs",cache:"no-cache",useCredentials:!0})}e.assets=[...N,...$,...t]}async function ie(e){const t=await re(e);return await t.pendingDownloadInternal.response,t.buffer}async function re(e){try{return await se(e)}catch(t){if(!Oe.enableDownloadRetry)throw t;if(Ce||Ae)throw t;if(e.pendingDownload&&e.pendingDownloadInternal==e.pendingDownload)throw t;if(e.resolvedUrl&&-1!=e.resolvedUrl.indexOf("file://"))throw t;if(t&&404==t.status)throw t;e.pendingDownloadInternal=void 0,await Oe.allDownloadsQueued.promise;try{return Oe.diagnosticTracing&&b(`Retrying download '${e.name}'`),await se(e)}catch(t){return e.pendingDownloadInternal=void 0,await new Promise(e=>globalThis.setTimeout(e,100)),Oe.diagnosticTracing&&b(`Retrying download (2) '${e.name}' after delay`),await se(e)}}}async function se(e){for(;U;)await U.promise;try{++L,L==Oe.maxParallelDownloads&&(Oe.diagnosticTracing&&b("Throttling further parallel downloads"),U=i());const t=await async function(e){if(e.pendingDownload&&(e.pendingDownloadInternal=e.pendingDownload),e.pendingDownloadInternal&&e.pendingDownloadInternal.response)return e.pendingDownloadInternal.response;if(e.buffer){const t=await e.buffer;return e.resolvedUrl||(e.resolvedUrl="undefined://"+e.name),e.pendingDownloadInternal={url:e.resolvedUrl,name:e.name,response:Promise.resolve({ok:!0,arrayBuffer:()=>t,json:()=>JSON.parse(new TextDecoder("utf-8").decode(t)),text:()=>{throw new Error("NotImplementedException")},headers:{get:()=>{}}})},e.pendingDownloadInternal.response}const t=e.loadRemote&&Oe.config.remoteSources?Oe.config.remoteSources:[""];let o;for(let n of t){n=n.trim(),"./"===n&&(n="");const t=ae(e,n);e.name===t?Oe.diagnosticTracing&&b(`Attempting to download '${t}'`):Oe.diagnosticTracing&&b(`Attempting to download '${t}' for ${e.name}`);try{e.resolvedUrl=t;const n=ue(e);if(e.pendingDownloadInternal=n,o=await n.response,!o||!o.ok)continue;return o}catch(e){o||(o={ok:!1,url:t,status:0,statusText:""+e});continue}}const n=e.isOptional||e.name.match(/\.pdb$/)&&Oe.config.ignorePdbLoadErrors;if(o||Fe(!1,`Response undefined ${e.name}`),!n){const t=new Error(`download '${o.url}' for ${e.name} failed ${o.status} ${o.statusText}`);throw t.status=o.status,t}w(`optional download '${o.url}' for ${e.name} failed ${o.status} ${o.statusText}`)}(e);return t?(J[e.behavior]||(e.buffer=await t.arrayBuffer(),++Oe.actual_downloaded_assets_count),e):e}finally{if(--L,U&&L==Oe.maxParallelDownloads-1){Oe.diagnosticTracing&&b("Resuming more parallel downloads");const e=U;U=void 0,e.promise_control.resolve()}}}function ae(e,t){let o;return null==t&&Fe(!1,`sourcePrefix must be provided for ${e.name}`),e.resolvedUrl?o=e.resolvedUrl:(o=""===t?"assembly"===e.behavior||"pdb"===e.behavior?e.name:"resource"===e.behavior&&e.culture&&""!==e.culture?`${e.culture}/${e.name}`:e.name:t+e.name,o=le(Oe.locateFile(o),e.behavior)),o&&"string"==typeof o||Fe(!1,"attemptUrl need to be path or url string"),o}function le(e,t){return Oe.modulesUniqueQuery&&V[t]&&(e+=Oe.modulesUniqueQuery),e}let ce=0;const de=new Set;function ue(e){try{e.resolvedUrl||Fe(!1,"Request's resolvedUrl must be set");const t=function(e){let t=e.resolvedUrl;if(Oe.loadBootResource){const o=me(e);if(o instanceof Promise)return o;"string"==typeof o&&(t=o)}const o={};return e.cache?o.cache=e.cache:Oe.config.disableNoCacheFetch||(o.cache="no-cache"),e.useCredentials?o.credentials="include":!Oe.config.disableIntegrityCheck&&e.hash&&(o.integrity=e.hash),Oe.fetch_like(t,o)}(e),o={name:e.name,url:e.resolvedUrl,response:t};return de.add(e.name),o.response.then(()=>{"assembly"==e.behavior&&Oe.loadedAssemblies.push(e.name),ce++,Oe.onDownloadResourceProgress&&Oe.onDownloadResourceProgress(ce,de.size)}),o}catch(t){const o={ok:!1,url:e.resolvedUrl,status:500,statusText:"ERR29: "+t,arrayBuffer:()=>{throw t},json:()=>{throw t}};return{name:e.name,url:e.resolvedUrl,response:Promise.resolve(o)}}}const fe={resource:"assembly",assembly:"assembly",pdb:"pdb",icu:"globalization",vfs:"configuration",manifest:"manifest",dotnetwasm:"dotnetwasm","js-module-dotnet":"dotnetjs","js-module-native":"dotnetjs","js-module-runtime":"dotnetjs","js-module-threads":"dotnetjs"};function me(e){var t;if(Oe.loadBootResource){const o=null!==(t=e.hash)&&void 0!==t?t:"",n=e.resolvedUrl,i=fe[e.behavior];if(i){const t=Oe.loadBootResource(i,e.name,n,o,e.behavior);return"string"==typeof t?C(t):t}}}function ge(e){e.pendingDownloadInternal=null,e.pendingDownload=null,e.buffer=null,e.moduleExports=null}function pe(e){let t=e.lastIndexOf("/");return t>=0&&t++,e.substring(t)}async function he(e){e&&await Promise.all((null!=e?e:[]).map(e=>async function(e){try{const t=e.name;if(!e.moduleExports){const o=le(Oe.locateFile(t),"js-module-library-initializer");Oe.diagnosticTracing&&b(`Attempting to import '${o}' for ${e}`),e.moduleExports=await import(/*! webpackIgnore: true */o)}Oe.libraryInitializers.push({scriptName:t,exports:e.moduleExports})}catch(t){v(`Failed to import library initializer '${e}': ${t}`)}}(e)))}async function be(e,t){if(!Oe.libraryInitializers)return;const o=[];for(let n=0;n<Oe.libraryInitializers.length;n++){const i=Oe.libraryInitializers[n];i.exports[e]&&o.push(we(i.scriptName,e,()=>i.exports[e](...t)))}await Promise.all(o)}async function we(e,t,o){try{await o()}catch(o){throw v(`Failed to invoke '${t}' on library initializer '${e}': ${o}`),Ke(1,o),o}}function ye(e,t){if(e===t)return e;const o={...t};return void 0!==o.assets&&o.assets!==e.assets&&(o.assets=[...e.assets||[],...o.assets||[]]),void 0!==o.resources&&(o.resources=_e(e.resources||{assembly:[],jsModuleNative:[],jsModuleRuntime:[],wasmNative:[]},o.resources)),void 0!==o.environmentVariables&&(o.environmentVariables={...e.environmentVariables||{},...o.environmentVariables||{}}),void 0!==o.runtimeOptions&&o.runtimeOptions!==e.runtimeOptions&&(o.runtimeOptions=[...e.runtimeOptions||[],...o.runtimeOptions||[]]),Object.assign(e,o)}function ve(e,t){if(e===t)return e;const o={...t};return o.config&&(e.config||(e.config={}),o.config=ye(e.config,o.config)),Object.assign(e,o)}function _e(e,t){if(e===t)return e;const o={...t};return void 0!==o.coreAssembly&&(o.coreAssembly=[...e.coreAssembly||[],...o.coreAssembly||[]]),void 0!==o.assembly&&(o.assembly=[...e.assembly||[],...o.assembly||[]]),void 0!==o.lazyAssembly&&(o.lazyAssembly=[...e.lazyAssembly||[],...o.lazyAssembly||[]]),void 0!==o.corePdb&&(o.corePdb=[...e.corePdb||[],...o.corePdb||[]]),void 0!==o.pdb&&(o.pdb=[...e.pdb||[],...o.pdb||[]]),void 0!==o.jsModuleWorker&&(o.jsModuleWorker=[...e.jsModuleWorker||[],...o.jsModuleWorker||[]]),void 0!==o.jsModuleNative&&(o.jsModuleNative=[...e.jsModuleNative||[],...o.jsModuleNative||[]]),void 0!==o.jsModuleDiagnostics&&(o.jsModuleDiagnostics=[...e.jsModuleDiagnostics||[],...o.jsModuleDiagnostics||[]]),void 0!==o.jsModuleRuntime&&(o.jsModuleRuntime=[...e.jsModuleRuntime||[],...o.jsModuleRuntime||[]]),void 0!==o.wasmSymbols&&(o.wasmSymbols=[...e.wasmSymbols||[],...o.wasmSymbols||[]]),void 0!==o.wasmNative&&(o.wasmNative=[...e.wasmNative||[],...o.wasmNative||[]]),void 0!==o.icu&&(o.icu=[...e.icu||[],...o.icu||[]]),void 0!==o.satelliteResources&&(o.satelliteResources=function(e,t){if(e===t)return e;for(const o in t)e[o]=[...e[o]||[],...t[o]||[]];return e}(e.satelliteResources||{},o.satelliteResources||{})),void 0!==o.modulesAfterConfigLoaded&&(o.modulesAfterConfigLoaded=[...e.modulesAfterConfigLoaded||[],...o.modulesAfterConfigLoaded||[]]),void 0!==o.modulesAfterRuntimeReady&&(o.modulesAfterRuntimeReady=[...e.modulesAfterRuntimeReady||[],...o.modulesAfterRuntimeReady||[]]),void 0!==o.extensions&&(o.extensions={...e.extensions||{},...o.extensions||{}}),void 0!==o.vfs&&(o.vfs=[...e.vfs||[],...o.vfs||[]]),Object.assign(e,o)}function Ee(){const e=Oe.config;if(e.environmentVariables=e.environmentVariables||{},e.runtimeOptions=e.runtimeOptions||[],e.resources=e.resources||{assembly:[],jsModuleNative:[],jsModuleWorker:[],jsModuleRuntime:[],wasmNative:[],vfs:[],satelliteResources:{}},e.assets){Oe.diagnosticTracing&&b("config.assets is deprecated, use config.resources instead");for(const t of e.assets){const o={};switch(t.behavior){case"assembly":o.assembly=[t];break;case"pdb":o.pdb=[t];break;case"resource":o.satelliteResources={},o.satelliteResources[t.culture]=[t];break;case"icu":o.icu=[t];break;case"symbols":o.wasmSymbols=[t];break;case"vfs":o.vfs=[t];break;case"dotnetwasm":o.wasmNative=[t];break;case"js-module-threads":o.jsModuleWorker=[t];break;case"js-module-runtime":o.jsModuleRuntime=[t];break;case"js-module-native":o.jsModuleNative=[t];break;case"js-module-diagnostics":o.jsModuleDiagnostics=[t];break;case"js-module-dotnet":break;default:throw new Error(`Unexpected behavior ${t.behavior} of asset ${t.name}`)}_e(e.resources,o)}}e.debugLevel,e.applicationEnvironment||(e.applicationEnvironment="Production"),e.applicationCulture&&(e.environmentVariables.LANG=`${e.applicationCulture}.UTF-8`),Ie.diagnosticTracing=Oe.diagnosticTracing=!!e.diagnosticTracing,Ie.waitForDebugger=e.waitForDebugger,Oe.maxParallelDownloads=e.maxParallelDownloads||Oe.maxParallelDownloads,Oe.enableDownloadRetry=void 0!==e.enableDownloadRetry?e.enableDownloadRetry:Oe.enableDownloadRetry}let Re=!1;async function je(e){var t;if(Re)return void await Oe.afterConfigLoaded.promise;let o;try{if(e.configSrc||Oe.config&&0!==Object.keys(Oe.config).length&&(Oe.config.assets||Oe.config.resources)||(e.configSrc="dotnet.boot.js"),o=e.configSrc,Re=!0,o&&(Oe.diagnosticTracing&&b("mono_wasm_load_config"),await async function(e){const t=e.configSrc,o=Oe.locateFile(t);let n=null;void 0!==Oe.loadBootResource&&(n=Oe.loadBootResource("manifest",t,o,"","manifest"));let i,r=null;if(n)if("string"==typeof n)n.includes(".json")?(r=await s(C(n)),i=await xe(r)):i=(await import(C(n))).config;else{const e=await n;"function"==typeof e.json?(r=e,i=await xe(r)):i=e.config}else o.includes(".json")?(r=await s(le(o,"manifest")),i=await xe(r)):i=(await import(le(o,"manifest"))).config;function s(e){return Oe.fetch_like(e,{method:"GET",credentials:"include",cache:"no-cache"})}Oe.config.applicationEnvironment&&(i.applicationEnvironment=Oe.config.applicationEnvironment),ye(Oe.config,i)}(e)),Ee(),await he(null===(t=Oe.config.resources)||void 0===t?void 0:t.modulesAfterConfigLoaded),await be("onRuntimeConfigLoaded",[Oe.config]),e.onConfigLoaded)try{await e.onConfigLoaded(Oe.config,Ue),Ee()}catch(e){throw _("onConfigLoaded() failed",e),e}Ee(),Oe.afterConfigLoaded.promise_control.resolve(Oe.config)}catch(t){const n=`Failed to load config file ${o} ${t} ${null==t?void 0:t.stack}`;throw Oe.config=e.config=Object.assign(Oe.config,{message:n,error:t,isError:!0}),Ke(1,new Error(n)),t}}function Te(){return!!globalThis.navigator&&(Oe.isChromium||Oe.isFirefox)}async function xe(e){const t=Oe.config,o=await e.json();t.applicationEnvironment||o.applicationEnvironment||(o.applicationEnvironment=e.headers.get("Blazor-Environment")||e.headers.get("DotNet-Environment")||void 0),o.environmentVariables||(o.environmentVariables={});const n=e.headers.get("DOTNET-MODIFIABLE-ASSEMBLIES");n&&(o.environmentVariables.DOTNET_MODIFIABLE_ASSEMBLIES=n);const i=e.headers.get("ASPNETCORE-BROWSER-TOOLS");return i&&(o.environmentVariables.__ASPNETCORE_BROWSER_TOOLS=i),o}"function"==typeof importScripts&&(globalThis.dotnetSidecar=!0);const Ae="object"==typeof process&&"object"==typeof process.versions&&"string"==typeof process.versions.node,Se="function"==typeof importScripts,De=Se&&"undefined"!=typeof dotnetSidecar,ke=Se&&!De,Me="object"==typeof window||Se&&!Ae,Ce=!Me&&!Ae;let Ie={},Oe={},Pe={},Ue={},Le={},Ne=!1;const $e={},ze={config:$e},We={mono:{},binding:{},internal:Le,module:ze,loaderHelpers:Oe,runtimeHelpers:Ie,diagnosticHelpers:Pe,api:Ue};function Fe(e,t){if(e)return;const o="Assert failed: "+("function"==typeof t?t():t),n=new Error(o);_(o,n),Ie.nativeAbort(n)}function Be(){return void 0!==Oe.exitCode}function Ve(){return Ie.runtimeReady&&!Be()}function He(){Be()&&Fe(!1,`.NET runtime already exited with ${Oe.exitCode} ${Oe.exitReason}. You can use dotnet.runMain() which doesn't exit the runtime.`),Ie.runtimeReady||Fe(!1,".NET runtime didn't start yet. Please call dotnet.create() first.")}function Je(){Me&&(globalThis.addEventListener("unhandledrejection",Ye),globalThis.addEventListener("error",et))}let qe,Qe;function Ge(e){Qe&&Qe(e),Ke(e,Oe.exitReason)}function Ze(e){qe&&qe(e||Oe.exitReason),Ke(1,e||Oe.exitReason)}function Ke(e,t){var o;const n=t&&"object"==typeof t;e=n&&"number"==typeof t.status?t.status:void 0===e?-1:e;const i=n&&"string"==typeof t.message?t.message:""+t;(t=n?t:Ie.ExitStatus?function(e,t){const o=new Ie.ExitStatus(e);return o.message=t,o.toString=()=>t,o}(e,i):new Error("Exit with code "+e+" "+i)).status=e,t.message||(t.message=i);const r=""+(t.stack||(new Error).stack);try{Object.defineProperty(t,"stack",{get:()=>r})}catch(e){}const s=!!t.silent;if(t.silent=!0,Be())Oe.diagnosticTracing&&b("mono_exit called after exit");else{try{ze.onAbort==Ze&&(ze.onAbort=qe),ze.onExit==Ge&&(ze.onExit=Qe),Me&&(globalThis.removeEventListener("unhandledrejection",Ye),globalThis.removeEventListener("error",et)),Ie.runtimeReady?(Ie.jiterpreter_dump_stats&&Ie.jiterpreter_dump_stats(!1),0===e&&(null===(o=Oe.config)||void 0===o?void 0:o.interopCleanupOnExit)&&Ie.forceDisposeProxies(!0,!0)):(Oe.diagnosticTracing&&b(`abort_startup, reason: ${t}`),function(e){Oe.allDownloadsQueued.promise_control.reject(e),Oe.allDownloadsFinished.promise_control.reject(e),Oe.afterConfigLoaded.promise_control.reject(e),Oe.wasmCompilePromise.promise_control.reject(e),Oe.runtimeModuleLoaded.promise_control.reject(e),Ie.dotnetReady&&(Ie.dotnetReady.promise_control.reject(e),Ie.afterInstantiateWasm.promise_control.reject(e),Ie.afterPreRun.promise_control.reject(e),Ie.beforeOnRuntimeInitialized.promise_control.reject(e),Ie.afterOnRuntimeInitialized.promise_control.reject(e),Ie.afterPostRun.promise_control.reject(e))}(t))}catch(e){v("mono_exit A failed",e)}try{s||(function(e,t){if(0!==e&&t){const e=Ie.ExitStatus&&t instanceof Ie.ExitStatus?b:_;"string"==typeof t?e(t):(void 0===t.stack&&(t.stack=(new Error).stack+""),t.message?e(Ie.stringify_as_error_with_stack?Ie.stringify_as_error_with_stack(t.message+"\n"+t.stack):t.message+"\n"+t.stack):e(JSON.stringify(t)))}!ke&&Oe.config&&(Oe.config.logExitCode?Oe.config.forwardConsole?j("WASM EXIT "+e):y("WASM EXIT "+e):Oe.config.forwardConsole&&j())}(e,t),function(e){if(Me&&!ke&&Oe.config&&Oe.config.appendElementOnExit&&document){const t=document.createElement("label");t.id="tests_done",0!==e&&(t.style.background="red"),t.innerHTML=""+e,document.body.appendChild(t)}}(e))}catch(e){v("mono_exit B failed",e)}Oe.exitCode=e,Oe.exitReason||(Oe.exitReason=t),!ke&&Ie.runtimeReady&&ze.runtimeKeepalivePop()}if(Oe.config&&Oe.config.asyncFlushOnExit&&0===e)throw(async()=>{try{await async function(){try{const e=await import(/*! webpackIgnore: true */"process"),t=e=>new Promise((t,o)=>{e.on("error",o),e.end("","utf8",t)}),o=t(e.stderr),n=t(e.stdout);let i;const r=new Promise(e=>{i=setTimeout(()=>e("timeout"),1e3)});await Promise.race([Promise.all([n,o]),r]),clearTimeout(i)}catch(e){_(`flushing std* streams failed: ${e}`)}}()}finally{Xe(e,t)}})(),t;Xe(e,t)}function Xe(e,t){if(Ie.runtimeReady&&Ie.nativeExit)try{Ie.nativeExit(e)}catch(e){!Ie.ExitStatus||e instanceof Ie.ExitStatus||v("set_exit_code_and_quit_now failed: "+e.toString())}if(0!==e||!Me)throw Ae?process.exit(e):Ie.quit&&Ie.quit(e,t),t}function Ye(e){tt(e,e.reason,"rejection")}function et(e){tt(e,e.error,"error")}function tt(e,t,o){e.preventDefault();try{t||(t=new Error("Unhandled "+o)),void 0===t.stack&&(t.stack=(new Error).stack),t.stack=t.stack+"",t.silent||(_("Unhandled error:",t),Ke(1,t))}catch(e){}}!function(n){if(Ne)throw new Error("Loader module already loaded");Ne=!0,Ie=n.runtimeHelpers,Oe=n.loaderHelpers,Pe=n.diagnosticHelpers,Ue=n.api,Le=n.internal,Object.assign(Ue,{INTERNAL:Le,invokeLibraryInitializers:be}),Object.assign(n.module,{config:ye($e,{environmentVariables:{}})});const a={mono_wasm_bindings_is_ready:!1,config:n.module.config,diagnosticTracing:!1,nativeAbort:e=>{throw e||new Error("abort")},nativeExit:e=>{throw new Error("exit:"+e)}},l={gitHash:"87bc0b04e21d786669142109a5128c95618b75ed",config:n.module.config,diagnosticTracing:!1,maxParallelDownloads:16,enableDownloadRetry:!0,_loaded_files:[],loadedFiles:[],loadedAssemblies:[],libraryInitializers:[],workerNextNumber:1,actual_downloaded_assets_count:0,actual_instantiated_assets_count:0,expected_downloaded_assets_count:0,expected_instantiated_assets_count:0,afterConfigLoaded:i(),allDownloadsQueued:i(),allDownloadsFinished:i(),wasmCompilePromise:i(),runtimeModuleLoaded:i(),loadingWorkers:i(),is_exited:Be,is_runtime_running:Ve,assert_runtime_running:He,mono_exit:Ke,createPromiseController:i,getPromiseController:r,assertIsControllablePromise:s,mono_download_assets:te,resolve_single_asset_path:Y,setup_proxy_console:R,set_thread_prefix:h,installUnhandledErrorHandler:Je,retrieve_asset_download:ie,invokeLibraryInitializers:be,isDebuggingSupported:Te,exceptions:e,simd:o,relaxedSimd:t};Object.assign(Ie,a),Object.assign(Oe,l)}(We);let ot,nt,it,rt=!1,st=!1;async function at(e){if(!st){if(st=!0,Me&&Oe.config.forwardConsole&&void 0!==globalThis.WebSocket&&R("main",globalThis.console,globalThis.location.origin),ze||Fe(!1,"Null moduleConfig"),Oe.config||Fe(!1,"Null moduleConfig.config"),"function"==typeof e){const t=e(We.api);if(t.ready)throw new Error("Module.ready couldn't be redefined.");Object.assign(ze,t),ve(ze,t)}else{if("object"!=typeof e)throw new Error("Can't use moduleFactory callback of createDotnetRuntime function.");ve(ze,e)}await async function(e){if(Ae){const e=await import(/*! webpackIgnore: true */"process"),t=14;if(e.versions.node.split(".")[0]<t)throw new Error(`NodeJS at '${e.execPath}' has too low version '${e.versions.node}', please use at least ${t}. See also https://aka.ms/dotnet-wasm-features`)}const t=/*! webpackIgnore: true */import.meta.url,o=t.indexOf("?");var n;if(o>0&&(Oe.modulesUniqueQuery=t.substring(o)),Oe.scriptUrl=t.replace(/\\/g,"/").replace(/[?#].*/,""),Oe.scriptDirectory=(n=Oe.scriptUrl).slice(0,n.lastIndexOf("/"))+"/",Oe.locateFile=e=>"URL"in globalThis&&globalThis.URL!==k?new URL(e,Oe.scriptDirectory).toString():P(e)?e:Oe.scriptDirectory+e,Oe.fetch_like=M,Oe.out=console.log,Oe.err=console.error,Oe.onDownloadResourceProgress=e.onDownloadResourceProgress,Me&&globalThis.navigator){const e=globalThis.navigator,t=e.userAgentData&&e.userAgentData.brands;t&&t.length>0?Oe.isChromium=t.some(e=>"Google Chrome"===e.brand||"Microsoft Edge"===e.brand||"Chromium"===e.brand):e.userAgent&&(Oe.isChromium=e.userAgent.includes("Chrome"),Oe.isFirefox=e.userAgent.includes("Firefox"))}void 0===globalThis.URL&&(globalThis.URL=k)}(ze)}}async function lt(e){return await at(e),Oe.config.exitOnUnhandledError&&Je(),qe=ze.onAbort,Qe=ze.onExit,ze.onAbort=Ze,ze.onExit=Ge,ze.ENVIRONMENT_IS_PTHREAD?async function(){(function(){const e=new MessageChannel,t=e.port1,o=e.port2;t.addEventListener("message",e=>{var n,i;n=JSON.parse(e.data.config),i=JSON.parse(e.data.monoThreadInfo),rt?Oe.diagnosticTracing&&b("mono config already received"):(ye(Oe.config,n),Ie.monoThreadInfo=i,Ee(),Oe.diagnosticTracing&&b("mono config received"),rt=!0,Oe.afterConfigLoaded.promise_control.resolve(Oe.config),Me&&n.forwardConsole&&void 0!==globalThis.WebSocket&&Oe.setup_proxy_console("worker-idle",console,globalThis.location.origin)),t.close(),o.close()},{once:!0}),t.start(),self.postMessage({[a]:{monoCmd:"preload",port:o}},[o])})(),await Oe.afterConfigLoaded.promise,function(){const e=Oe.config;e.assets||Fe(!1,"config.assets must be defined");for(const t of e.assets)K(t),Q[t.behavior]&&$.push(t)}(),setTimeout(async()=>{try{await te()}catch(e){Ke(1,e)}},0);const e=ct(),t=await Promise.all(e);return await dt(t),ze}():async function(){var e;await je(ze),ne();const t=ct();(async function(){try{const e=Y("dotnetwasm");await re(e),e&&e.pendingDownloadInternal&&e.pendingDownloadInternal.response||Fe(!1,"Can't load dotnet.native.wasm");const t=await e.pendingDownloadInternal.response,o=t.headers&&t.headers.get?t.headers.get("Content-Type"):void 0;let n;if("function"==typeof WebAssembly.compileStreaming&&"application/wasm"===o)n=await WebAssembly.compileStreaming(t);else{Me&&"application/wasm"!==o&&v('WebAssembly resource does not have the expected content type "application/wasm", so falling back to slower ArrayBuffer instantiation.');const e=await t.arrayBuffer();Oe.diagnosticTracing&&b("instantiate_wasm_module buffered"),n=Ce?await Promise.resolve(new WebAssembly.Module(e)):await WebAssembly.compile(e)}e.pendingDownloadInternal=null,e.pendingDownload=null,e.buffer=null,e.moduleExports=null,Oe.wasmCompilePromise.promise_control.resolve(n)}catch(e){Oe.wasmCompilePromise.promise_control.reject(e)}})(),setTimeout(async()=>{try{S(),await te()}catch(e){Ke(1,e)}},0);const o=await Promise.all(t);return await dt(o),await Ie.dotnetReady.promise,await he(null===(e=Oe.config.resources)||void 0===e?void 0:e.modulesAfterRuntimeReady),await be("onRuntimeReady",[We.api]),Ue}()}function ct(){const e=Y("js-module-runtime"),t=Y("js-module-native");if(ot&&nt)return[ot,nt,it];"object"==typeof e.moduleExports?ot=e.moduleExports:(Oe.diagnosticTracing&&b(`Attempting to import '${e.resolvedUrl}' for ${e.name}`),ot=import(/*! webpackIgnore: true */e.resolvedUrl)),"object"==typeof t.moduleExports?nt=t.moduleExports:(Oe.diagnosticTracing&&b(`Attempting to import '${t.resolvedUrl}' for ${t.name}`),nt=import(/*! webpackIgnore: true */t.resolvedUrl));const o=X("js-module-diagnostics");return o&&("object"==typeof o.moduleExports?it=o.moduleExports:(Oe.diagnosticTracing&&b(`Attempting to import '${o.resolvedUrl}' for ${o.name}`),it=import(/*! webpackIgnore: true */o.resolvedUrl))),[ot,nt,it]}async function dt(e){const{initializeExports:t,initializeReplacements:o,configureRuntimeStartup:n,configureEmscriptenStartup:i,configureWorkerStartup:r,setRuntimeGlobals:s,passEmscriptenInternals:a}=e[0],{default:l}=e[1],c=e[2];s(We),t(We),c&&c.setRuntimeGlobals(We),await n(ze),Oe.runtimeModuleLoaded.promise_control.resolve(),l(()=>(Object.assign(ze,{__dotnet_runtime:{initializeReplacements:o,configureEmscriptenStartup:i,configureWorkerStartup:r,passEmscriptenInternals:a}}),ze)).catch(e=>{if(e.message&&e.message.toLowerCase().includes("out of memory"))throw new Error(".NET runtime has failed to start, because too much memory was requested. Please decrease the memory by adjusting EmccMaximumHeapSize. See also https://aka.ms/dotnet-wasm-features");throw e})}const ut=new class{withModuleConfig(e){try{return ve(ze,e),this}catch(e){throw Ke(1,e),e}}withInterpreterPgo(e,t){try{return ye($e,{interpreterPgo:e,interpreterPgoSaveDelay:t}),$e.runtimeOptions?$e.runtimeOptions.push("--interp-pgo-recording"):$e.runtimeOptions=["--interp-pgo-recording"],this}catch(e){throw Ke(1,e),e}}withConfig(e){try{return ye($e,e),this}catch(e){throw Ke(1,e),e}}withConfigSrc(e){try{return e&&"string"==typeof e||Fe(!1,"must be file path or URL"),ve(ze,{configSrc:e}),this}catch(e){throw Ke(1,e),e}}withVirtualWorkingDirectory(e){try{return e&&"string"==typeof e||Fe(!1,"must be directory path"),ye($e,{virtualWorkingDirectory:e}),this}catch(e){throw Ke(1,e),e}}withEnvironmentVariable(e,t){try{const o={};return o[e]=t,ye($e,{environmentVariables:o}),this}catch(e){throw Ke(1,e),e}}withEnvironmentVariables(e){try{return e&&"object"==typeof e||Fe(!1,"must be dictionary object"),ye($e,{environmentVariables:e}),this}catch(e){throw Ke(1,e),e}}withDiagnosticTracing(e){try{return"boolean"!=typeof e&&Fe(!1,"must be boolean"),ye($e,{diagnosticTracing:e}),this}catch(e){throw Ke(1,e),e}}withDebugging(e){try{return null!=e&&"number"==typeof e||Fe(!1,"must be number"),ye($e,{debugLevel:e}),this}catch(e){throw Ke(1,e),e}}withApplicationArguments(...e){try{return e&&Array.isArray(e)||Fe(!1,"must be array of strings"),ye($e,{applicationArguments:e}),this}catch(e){throw Ke(1,e),e}}withRuntimeOptions(e){try{return e&&Array.isArray(e)||Fe(!1,"must be array of strings"),$e.runtimeOptions?$e.runtimeOptions.push(...e):$e.runtimeOptions=e,this}catch(e){throw Ke(1,e),e}}withMainAssembly(e){try{return ye($e,{mainAssemblyName:e}),this}catch(e){throw Ke(1,e),e}}withApplicationArgumentsFromQuery(){try{if(!globalThis.window)throw new Error("Missing window to the query parameters from");if(void 0===globalThis.URLSearchParams)throw new Error("URLSearchParams is supported");const e=new URLSearchParams(globalThis.window.location.search).getAll("arg");return this.withApplicationArguments(...e)}catch(e){throw Ke(1,e),e}}withApplicationEnvironment(e){try{return ye($e,{applicationEnvironment:e}),this}catch(e){throw Ke(1,e),e}}withApplicationCulture(e){try{return ye($e,{applicationCulture:e}),this}catch(e){throw Ke(1,e),e}}withResourceLoader(e){try{return Oe.loadBootResource=e,this}catch(e){throw Ke(1,e),e}}async download(){try{await async function(){at(ze),await je(ze),ne(),S(),te(),await Oe.allDownloadsFinished.promise}()}catch(e){throw Ke(1,e),e}}async create(){try{return this.instance||(this.instance=await async function(){return await lt(ze),We.api}()),this.instance}catch(e){throw Ke(1,e),e}}run(){return this.runMainAndExit()}async runMainAndExit(){try{return ze.config||Fe(!1,"Null moduleConfig.config"),this.instance||await this.create(),this.instance.runMainAndExit()}catch(e){throw Ke(1,e),e}}async runMain(){try{return ze.config||Fe(!1,"Null moduleConfig.config"),this.instance||await this.create(),this.instance.runMain()}catch(e){throw Ke(1,e),e}}},ft=Ke,mt=lt;Ce||"function"==typeof globalThis.URL||Fe(!1,"This browser/engine doesn't support URL API. Please use a modern version. See also https://aka.ms/dotnet-wasm-features"),"function"!=typeof globalThis.BigInt64Array&&Fe(!1,"This browser/engine doesn't support BigInt64Array API. Please use a modern version. See also https://aka.ms/dotnet-wasm-features"),globalThis.performance&&"function"==typeof globalThis.performance.now||Fe(!1,"This browser/engine doesn't support performance.now. Please use a modern version."),Ce||globalThis.crypto&&"object"==typeof globalThis.crypto.subtle||Fe(!1,"This engine doesn't support crypto.subtle. Please use a modern version."),Ce||globalThis.crypto&&"function"==typeof globalThis.crypto.getRandomValues||Fe(!1,"This engine doesn't support crypto.getRandomValues. Please use a modern version."),Ae&&"function"!=typeof process.exit&&Fe(!1,"This engine doesn't support process.exit. Please use a modern version."),ut.withConfig(/*json-start*/{
  "mainAssemblyName": "CUIFlavoredPortfolioSite",
  "resources": {
    "hash": "sha256-pZQvBb2BVgufHknYPZXyt34dI6ub22wbxZ107Rf4dzU=",
    "jsModuleNative": [
      {
        "name": "dotnet.native.zvrqk3gpsq.js"
      }
    ],
    "jsModuleRuntime": [
      {
        "name": "dotnet.runtime.kl7gvzuey0.js"
      }
    ],
    "wasmNative": [
      {
        "name": "dotnet.native.dayx6uxni7.wasm",
        "hash": "sha256-KwZI+IFbt/THAEYkwLDLl0lG35qvjyYQuklQrq7lTIY=",
        "cache": "force-cache"
      }
    ],
    "coreAssembly": [
      {
        "virtualPath": "System.Runtime.InteropServices.JavaScript.wasm",
        "name": "System.Runtime.InteropServices.JavaScript.ke3048uapr.wasm",
        "hash": "sha256-PBWjRSA8I9kdCuOL4WGI25r/4BoDjm/iWVwrwvInGsI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Private.CoreLib.wasm",
        "name": "System.Private.CoreLib.lhvkyyvjz5.wasm",
        "hash": "sha256-abZog2WSmedHEY+VOYxx5GH08TK2k8tNP1CnF5Qm3Uo=",
        "cache": "force-cache"
      }
    ],
    "assembly": [
      {
        "virtualPath": "CommandLineSwitchParser.wasm",
        "name": "CommandLineSwitchParser.sr4z4qwcg5.wasm",
        "hash": "sha256-6C4lpQHhVm9zj9eSWImlbOfLMjZc4Kf2OUiJSjTpLkA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Figgle.wasm",
        "name": "Figgle.s8k3msdvya.wasm",
        "hash": "sha256-xCBIOWi/r/unhhhVJmmGeviYucogDlU74/E1Sdddgg8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Components.wasm",
        "name": "Microsoft.AspNetCore.Components.obb2bn1uw2.wasm",
        "hash": "sha256-LCOtWR5Eo57GWQrm+h2iOt6DBW9eRON7RTD7GIyYPvs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Components.Web.wasm",
        "name": "Microsoft.AspNetCore.Components.Web.y2w6iesg2f.wasm",
        "hash": "sha256-Fg5R9MwxIv3RrRaxyuN+TEBG8nhH/YIrvLoia7c5G8o=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.AspNetCore.Components.WebAssembly.wasm",
        "name": "Microsoft.AspNetCore.Components.WebAssembly.doii42bkx1.wasm",
        "hash": "sha256-81RP1LGrUeC4i4ehkML5s58iaOT1ebklUnPQUw54rIM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Configuration.wasm",
        "name": "Microsoft.Extensions.Configuration.uabncmb0ew.wasm",
        "hash": "sha256-9uzRXRTGHtoNN5a1JYArLZGDblEM1l5dOWd3LB1WjgQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Configuration.Abstractions.wasm",
        "name": "Microsoft.Extensions.Configuration.Abstractions.ooq5283nf9.wasm",
        "hash": "sha256-qJcr6Fc/50eJJvdEIRfzVuRP97MNbOEuEkoYtejBW/E=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Configuration.EnvironmentVariables.wasm",
        "name": "Microsoft.Extensions.Configuration.EnvironmentVariables.0b388xm8kh.wasm",
        "hash": "sha256-RLFYrxEIqE2KAwSoDvqEMs2VE2G9puXzNx1LFr13ivI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Configuration.Json.wasm",
        "name": "Microsoft.Extensions.Configuration.Json.82ym46gts0.wasm",
        "hash": "sha256-EA7fJKb0sPjWoURBvj5OxdCgJlZHQ14EAx4z+x25btk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.DependencyInjection.wasm",
        "name": "Microsoft.Extensions.DependencyInjection.548xns1hi0.wasm",
        "hash": "sha256-q6Gt/KII0v+SQ57aNKcSIPUBUSB62ZZ9qashqCSzj5g=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.DependencyInjection.Abstractions.wasm",
        "name": "Microsoft.Extensions.DependencyInjection.Abstractions.6millsohvc.wasm",
        "hash": "sha256-fjae9ms6uE1Me1fiXTjHeyS7sjz2z9F1NmEZ7EDY+xM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Diagnostics.wasm",
        "name": "Microsoft.Extensions.Diagnostics.ekdkqkw9pe.wasm",
        "hash": "sha256-UhT//7i0SIyM+ydPz2LqK+q/hpad/FTPVFvBV3N9BEM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Diagnostics.Abstractions.wasm",
        "name": "Microsoft.Extensions.Diagnostics.Abstractions.joqu1vpo7a.wasm",
        "hash": "sha256-k++WrWWwGfV97/wy0sYpOhR9C1Qn5eNW95uvfBoYRpQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Hosting.Abstractions.wasm",
        "name": "Microsoft.Extensions.Hosting.Abstractions.huej6dt4vh.wasm",
        "hash": "sha256-6buSBd4GbMBnyKArIKrIfIkOSKGgOA2dW3sj3znrEWI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Logging.wasm",
        "name": "Microsoft.Extensions.Logging.8gvwkdhrvs.wasm",
        "hash": "sha256-OB8yXW/nH14Z18tw0YtM9ISyWGixUwVVs1kN/j1e3Ck=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Logging.Abstractions.wasm",
        "name": "Microsoft.Extensions.Logging.Abstractions.b8zzwoqpyu.wasm",
        "hash": "sha256-G/2Ctk12WGAkSN+YghLIzrm2GDN1nV7Ou4dg2xuWJ+I=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Options.wasm",
        "name": "Microsoft.Extensions.Options.lmdq85cb2o.wasm",
        "hash": "sha256-g3w31+UBzK2umUT6/B7/4pIsS5dj8xXr/hbgv0yv17g=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Primitives.wasm",
        "name": "Microsoft.Extensions.Primitives.vm2ecztgxj.wasm",
        "hash": "sha256-KTMoCbwOIebzBMBYzZ3j7rmNgr766Zwws06jeFbKCqE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.JSInterop.wasm",
        "name": "Microsoft.JSInterop.wyrjux5u3w.wasm",
        "hash": "sha256-qBk40IkuTXeHQGhW2X2mMQDefn9Sjg1jUDUX3SOtaUo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.JSInterop.WebAssembly.wasm",
        "name": "Microsoft.JSInterop.WebAssembly.lhplwqly2s.wasm",
        "hash": "sha256-SQy7KL8yE/Ai6o/S3L8PgiBrcBYsoedhzVIJtPIfW9A=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Toolbelt.AnsiEscCode.Colorize.wasm",
        "name": "Toolbelt.AnsiEscCode.Colorize.rzopx1zxmr.wasm",
        "hash": "sha256-OeDtBknZYfLD+LbXrcmdd3WZFATGFGiv7pZ2hARtxTs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Toolbelt.Blazor.HotKeys2.wasm",
        "name": "Toolbelt.Blazor.HotKeys2.zb18o0z2ij.wasm",
        "hash": "sha256-v4DWIiE94uI7uw6X3QGPPFxjzvZU1tE/L1vAA5nj09A=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Toolbelt.Blazor.PWA.Updater.wasm",
        "name": "Toolbelt.Blazor.PWA.Updater.dw92v4tslz.wasm",
        "hash": "sha256-nX3bxJFDW2YeZsn83wYeKms+LDBH87E/NO27qCxCNx0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Toolbelt.Blazor.PWA.Updater.Service.wasm",
        "name": "Toolbelt.Blazor.PWA.Updater.Service.4k1ydz5me3.wasm",
        "hash": "sha256-ZKVIic82m+pTSlJmZN1C6Vct1s78oj+PlIPpVs8nwJY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Toolbelt.Web.CssClassInlineBuilder.wasm",
        "name": "Toolbelt.Web.CssClassInlineBuilder.l7du25fzii.wasm",
        "hash": "sha256-YcT8m49nLl4G2OZzSaoVXWqvS9ApmNGGzfxUi6tcPVY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.Concurrent.wasm",
        "name": "System.Collections.Concurrent.22tomotpq3.wasm",
        "hash": "sha256-Gu9jjxsfIFUTUn2o56nFHp2YmjWAUWOX0JIjJnEnyEE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.Immutable.wasm",
        "name": "System.Collections.Immutable.4cysud0szm.wasm",
        "hash": "sha256-1M5JJH2fAHhR/LfqSOQxj2OHypZY3LW8nXqFilR4bz8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.wasm",
        "name": "System.Collections.0j3uwmbura.wasm",
        "hash": "sha256-XNXn0c4Be4dQJTBAdbuwKLbJAg4SRZji5kzQwJIz6+Q=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ComponentModel.wasm",
        "name": "System.ComponentModel.663infdl5t.wasm",
        "hash": "sha256-jgOiL/Nj/WRJKK8ys6+t62ss1HGovDlBoyGRUq3c/zY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Console.wasm",
        "name": "System.Console.202ewwcpzn.wasm",
        "hash": "sha256-Xl7FS67c8C3Pb6/chaIHzxh32UYu8qcES4XJwI6kGf8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.DiagnosticSource.wasm",
        "name": "System.Diagnostics.DiagnosticSource.dg75zq2vjw.wasm",
        "hash": "sha256-/T8k7nVVPogZmRJlyYvMk+FCpjNn1qWYjdqtkGO+CTk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.Compression.wasm",
        "name": "System.IO.Compression.1dxhqb4r04.wasm",
        "hash": "sha256-29Z2KifEvKn/JIQR0wpQd/woj09/2m/hOhaWfAEmDO4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.Pipelines.wasm",
        "name": "System.IO.Pipelines.9o0bktsgw7.wasm",
        "hash": "sha256-1z/9vAnY3n7LI0+1FVp9u/kRhp6UvIUyZbgQo2S8QJQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Linq.wasm",
        "name": "System.Linq.ys2yw8om1m.wasm",
        "hash": "sha256-Tn1SLa1/hqOlnzINxG01Z86oShNjKucFaaUtiZzB/RA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Memory.wasm",
        "name": "System.Memory.7r9nfi4m29.wasm",
        "hash": "sha256-3bejX6853tOSEnHdJkACcMO00zYDevxYxWXONOrry4A=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Http.wasm",
        "name": "System.Net.Http.vu0nc7d2mi.wasm",
        "hash": "sha256-F7wcn94RTKBYjsaSnifp9DOSxCnji9Z+F1L3BVm2y6E=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Primitives.wasm",
        "name": "System.Net.Primitives.dg2qzsmww2.wasm",
        "hash": "sha256-Ex5n8AWA+Z6jFbkPi77abOG0YFHL/ba3SAbrikj0R2Y=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Private.Uri.wasm",
        "name": "System.Private.Uri.4gzowfd25b.wasm",
        "hash": "sha256-jCr5EeIu8+uGetpmvltxRhBvuzWMIMh3pDBS7XhYTX0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.Extensions.wasm",
        "name": "System.Reflection.Extensions.982djoym21.wasm",
        "hash": "sha256-iT/vmMoL09Z5glxnx74+Xw7d3kAFc4qfeVUP3BQLAyM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.wasm",
        "name": "System.Reflection.cdscl6ldea.wasm",
        "hash": "sha256-YEihvvPCyt7ZBclpTEcURGG4AdqfXOPSrG/wgaGOREw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Extensions.wasm",
        "name": "System.Runtime.Extensions.qqpel0hekv.wasm",
        "hash": "sha256-0j6sMnLzs1FeQ6Q9WGx9imjWgTx6w+BVABZ0J4AYPp0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.wasm",
        "name": "System.Runtime.10onqk2ppp.wasm",
        "hash": "sha256-v5wACyC/69jktsIdvqioo6Bsm31ETiu8Wea2mqoc2Ec=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.Cryptography.wasm",
        "name": "System.Security.Cryptography.c25j2nnxy2.wasm",
        "hash": "sha256-ZE7wqlIHcYwVYrIoTPDm7Zi39UzYWvgb+z6/2WfypIo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Text.Encodings.Web.wasm",
        "name": "System.Text.Encodings.Web.dfc2b0g5f0.wasm",
        "hash": "sha256-lynNGWNoAFvlKSyhDWskDqFswwR8Ga0Cx8oRckyms98=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Text.Json.wasm",
        "name": "System.Text.Json.q42ulkxm21.wasm",
        "hash": "sha256-SvvkZyOKNa8c/wiPMPNozbWSOeXFxen9YHGNEd5uPek=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Text.RegularExpressions.wasm",
        "name": "System.Text.RegularExpressions.k79xbiucpz.wasm",
        "hash": "sha256-4P/0fynrV5MNHyGJVI1srQXicCqw2/2Sx7nzWUb/oXA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.wasm",
        "name": "System.Threading.7zt6fsxsj5.wasm",
        "hash": "sha256-vbMJVx2XHAfRUCoAMw8/FDm6GwkyX+uG89WDSPcpls4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "netstandard.wasm",
        "name": "netstandard.xvfttciqj0.wasm",
        "hash": "sha256-rYLgKYh+lkkwmPyNPchTGuERA+mtckc5sW9Gwb/MLuU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "CUIFlavoredPortfolioSite.wasm",
        "name": "CUIFlavoredPortfolioSite.50t3sj24ba.wasm",
        "hash": "sha256-a2ILykDJhfDdOhyIdKnPJN6awMQoNEPSD5pgAhjFD+o=",
        "cache": "force-cache"
      }
    ],
    "libraryInitializers": [
      {
        "name": "BlazorWasmPreRendering.Build.lfyg69o9wu.lib.module.js"
      }
    ],
    "modulesAfterConfigLoaded": [
      {
        "name": "../BlazorWasmPreRendering.Build.lfyg69o9wu.lib.module.js"
      }
    ]
  },
  "debugLevel": 0,
  "globalizationMode": "invariant",
  "extensions": {
    "blazor": {}
  },
  "runtimeConfig": {
    "runtimeOptions": {
      "configProperties": {
        "Microsoft.AspNetCore.Components.Routing.RegexConstraintSupport": false,
        "Toolbelt.Blazor.HotKeys2.OptimizeForWasm": true,
        "Toolbelt.Blazor.HotKeys2.JavaScriptCacheBusting": true,
        "Microsoft.Extensions.DependencyInjection.VerifyOpenGenericServiceTrimmability": true,
        "System.ComponentModel.DefaultValueAttribute.IsSupported": false,
        "System.ComponentModel.Design.IDesignerHost.IsSupported": false,
        "System.ComponentModel.TypeConverter.EnableUnsafeBinaryFormatterInDesigntimeLicenseContextSerialization": false,
        "System.ComponentModel.TypeDescriptor.IsComObjectDescriptorSupported": false,
        "System.Data.DataSet.XmlSerializationIsSupported": false,
        "System.Diagnostics.Debugger.IsSupported": false,
        "System.Diagnostics.Metrics.Meter.IsSupported": false,
        "System.Diagnostics.Tracing.EventSource.IsSupported": false,
        "System.GC.Server": true,
        "System.Globalization.Invariant": true,
        "System.TimeZoneInfo.Invariant": false,
        "System.Globalization.PredefinedCulturesOnly": true,
        "System.Linq.Enumerable.IsSizeOptimized": true,
        "System.Net.Http.EnableActivityPropagation": false,
        "System.Net.Http.WasmEnableStreamingResponse": true,
        "System.Net.SocketsHttpHandler.Http3Support": false,
        "System.Reflection.Metadata.MetadataUpdater.IsSupported": false,
        "System.Resources.ResourceManager.AllowCustomResourceTypes": false,
        "System.Resources.UseSystemResourceKeys": true,
        "System.Runtime.CompilerServices.RuntimeFeature.IsDynamicCodeSupported": true,
        "System.Runtime.InteropServices.BuiltInComInterop.IsSupported": false,
        "System.Runtime.InteropServices.EnableConsumingManagedCodeFromNativeHosting": false,
        "System.Runtime.InteropServices.EnableCppCLIHostActivation": false,
        "System.Runtime.InteropServices.Marshalling.EnableGeneratedComInterfaceComImportInterop": false,
        "System.Runtime.Serialization.EnableUnsafeBinaryFormatterSerialization": false,
        "System.StartupHookProvider.IsSupported": false,
        "System.Text.Encoding.EnableUnsafeUTF7Encoding": false,
        "System.Text.Json.JsonSerializer.IsReflectionEnabledByDefault": true,
        "System.Threading.Thread.EnableAutoreleasePool": false,
        "Microsoft.AspNetCore.Components.Endpoints.NavigationManager.DisableThrowNavigationException": false
      }
    }
  }
}/*json-end*/);export{mt as default,ut as dotnet,ft as exit};
