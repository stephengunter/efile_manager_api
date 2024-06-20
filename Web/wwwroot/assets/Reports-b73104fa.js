import{_ as Z}from"./Date-e8692911.js";import{V as U}from"./validate.types-512aac96.js";import{bc as C,n as F,r as H,d as E,c as M,o as X,bd as P,e as k,h as N,w as d,g as o,u as ee,a as te,b as ae,K as le,be as L,a2 as se,f as I,k as W,aX as oe,bf as re,aW as G,a8 as O,q as K,I as q,v as A,V as de,j as n,t as c,F as ne,p as ie,b1 as ce,l as ue,av as j,b3 as fe,i as J,bg as me,bb as _e,aw as Y,A as z,bh as pe,E as ve}from"./index-c3e40adb.js";import{a as f,V as T}from"./VRow-39a21267.js";import{V as Q}from"./VTextField-e1b68920.js";import{b as he,g as be,d as ge}from"./files-2d334451.js";const De={__name:"Period",props:{roc:{type:Boolean,default:!1},values:{type:Array,default:()=>["",""]},dates:{type:Array,default:()=>[null,null]},labels:{type:Array,default:()=>["開始日期","結束日期"]},required_start:{type:Boolean,default:!1},required_end:{type:Boolean,default:!1},allow_same:{type:Boolean,default:!0}},emits:["selected"],setup(m,{expose:h,emit:w}){const p=new C.adapter({locale:C.locale.zhTW}),u=m,R=w;h({getDates:y});const e={dates:[null,null],values:["",""],models:[null,null],errors:new F},l=H(E(e)),b=M(()=>l.dates[0]?l.dates[0]:null),v=M(()=>l.dates[1]?l.dates[1]:null);X(g);function g(){l.dates=u.dates.slice(0),l.values=u.values.slice(0),l.models=u.dates.map(i=>P(i,u.roc))}function D(i,{date:t,model:a},s=!0){l.dates[i]=t,l.models[i]=a,l.values[i]=u.roc?a.text_cn:a.text,s&&B()&&(console.log(y()),R("selected",y()))}function V(i){if(i){if(b.value&&(p.isBefore(i,b.value)||p.isEqual(i,b.value)&&!u.allow_same))return"lower_limit";if(v.value&&(p.isAfter(i,v.value)||p.isEqual(i,v.value)&&!u.allow_same))return"upper_limit"}return""}function B(){l.errors=new F,u.required_start&&(l.dates[0]||l.errors.set("start",[`${U.REQUIRED(u.labels[0])}`])),u.required_end&&!l.end&&l.errors.set("end",[`${U.REQUIRED(u.labels[0])}`]);const i=l.dates[0],t=V(i);t&&l.errors.set("start",[t]);const a=l.dates[1],s=V(a);return s&&l.errors.set("end",[s]),!l.errors.any()}function y(){return l.dates.map((i,t)=>({date:i,model:l.models[t]}))}return(i,t)=>{const a=Z;return k(),N(T,{dense:""},{default:d(()=>[o(f,{cols:"6"},{default:d(()=>[o(a,{roc:m.roc,label:m.labels[0],date:l.dates[0],value:l.values[0],upper_limit:v.value,clearable:!m.required_start,error_message:l.errors.get("start"),onReady:t[0]||(t[0]=s=>D(0,s,!1)),onSelected:t[1]||(t[1]=s=>D(0,s))},null,8,["roc","label","date","value","upper_limit","clearable","error_message"])]),_:1}),o(f,{cols:"6"},{default:d(()=>[o(a,{roc:m.roc,label:m.labels[1],date:l.dates[1],value:l.values[1],lower_limit:b.value,clearable:!m.required_start,error_message:l.errors.get("end"),onReady:t[2]||(t[2]=s=>D(1,s,!1)),onSelected:t[3]||(t[3]=s=>D(1,s))},null,8,["roc","label","date","value","lower_limit","clearable","error_message"])]),_:1})]),_:1})}}},ke={style:{width:"10%"}},we={style:{width:"10%"}},ye={style:{width:"15%"}},xe={style:{width:"10%"}},Ee={style:{width:"25%"}},Pe={style:{width:"10%"}},Te=n("th",{style:{width:"10%"}}," 下載紀錄 ",-1),Re={class:"font-weight-bold"},Ve={class:"font-weight-bold"},Ae={class:"font-weight-bold"},je={class:"font-weight-bold"},Ce={class:"font-weight-bold"},Be={class:"font-weight-bold"},Se=["onClick"],$e={class:"font-weight-bold"},Ie=["onClick"],Oe=["textContent"],We={__name:"Reports",setup(m){const h=ee();te(),ae();const w=new C.adapter({locale:C.locale.zhTW}),p=le(null),u=ve.JUDGEBOOKFILE,R={active:!1,params:{createdAt:"",judgeDate:""},createdDates:[],judgeDates:[],datePicker:{roc:!1,labels:[],dates:[],values:[],required_start:!1,required_end:!1,allow_same:!0,key:"",title:"",lower_limit:null,upper_limit:null,active:!1},list:[],comments:""},e=H(E(R)),l=M(()=>h.state.files_judgebooks.labels);X(b);function b(){const t=w.date(),a=w.startOfMonth(t),s=w.endOfMonth(t);e.createdDates.push({text:L(a),date:a,model:P(a)}),e.createdDates.push({text:L(s),date:s,model:P(s)}),e.judgeDates.push({text:"",date:null,model:P()}),e.judgeDates.push({text:"",date:null,model:P()}),v("createdAt")}function v(t){let a="",s="";t==="createdAt"?(a=e.createdDates[0].text,s=e.createdDates[1].text):t==="judgeDate"&&(a=e.judgeDates[0].model.text_cn,s=e.judgeDates[1].model.text_cn),a&&s?e.params[t]=`${a} ~ ${s}`:a?e.params[t]=`${a} ~ `:s?e.params[t]=` ~ ${s}`:e.params[t]="",se(V)}function g(t){if(!t){e.datePicker=E(R.datePicker);return}t==="createdAt"?(e.datePicker.roc=!1,e.datePicker.dates=e.createdDates.map(a=>a.date),e.datePicker.values=e.createdDates.map(a=>a.text),e.datePicker.labels=["起始日期","截止日期"],e.datePicker.title=`選擇${l.value.createdAtText}區間`):t==="judgeDate"&&(e.datePicker.roc=!0,e.datePicker.dates=e.judgeDates.map(a=>a.date),e.datePicker.values=e.judgeDates.map(a=>a.text),e.datePicker.labels=["起始日期","截止日期"],e.datePicker.title=`選擇${l.value.judgeDate}區間`),e.datePicker.key=t,e.datePicker.active=!0}function D(){const t=p.value.getDates(),a=e.datePicker.key;a==="createdAt"?e.createdDates=t.map(s=>({date:s.date,text:s.model.text,model:E(s.model)})):a==="judgeDate"&&(e.judgeDates=t.map(s=>({date:s.date,text:s.model.text_cn,model:E(s.model)}))),v(a),g()}function V(){e.comments="",h.dispatch(re,e.params).then(t=>{e.list=t,e.comments=`合計： ${t.length} 件`}).catch(t=>G(t))}function B(t){Y({type:u.name,id:t,action:z.REVIEW.name,title:`${z.REVIEW.title}紀錄`,width:j.L})}function y(t){console.log(t.modifyRecords),h.commit(pe,t.modifyRecords),Y({title:"下載紀錄",width:j.L})}function i(){const t=e.list.map(_=>_.id),a=e.params.createdAt,s=e.params.judgeDate;h.dispatch(me,{ids:t,reviewedAt:a,judgeDate:s}).then(_=>{const r=".pdf",S=_.fileName,x=he(_.fileBytes),$=new Blob([x],{type:be(r)});ge($,S)}).catch(_=>G(_))}return(t,a)=>{const s=_e,_=De;return k(),I("div",null,[o(T,{dense:""},{default:d(()=>[o(f,{cols:"2"},{default:d(()=>[o(Q,{label:`${l.value.createdAtText}區間`,variant:"outlined",readonly:"",density:"compact",modelValue:e.params.createdAt,"onUpdate:modelValue":a[0]||(a[0]=r=>e.params.createdAt=r),"onClick:control":a[1]||(a[1]=r=>g("createdAt"))},null,8,["label","modelValue"])]),_:1}),o(f,{cols:"2"},{default:d(()=>[o(Q,{label:`${l.value.judgeDate}區間`,variant:"outlined",readonly:"",density:"compact",modelValue:e.params.judgeDate,"onUpdate:modelValue":a[2]||(a[2]=r=>e.params.judgeDate=r),"onClick:control":a[3]||(a[3]=r=>g("judgeDate"))},null,8,["label","modelValue"])]),_:1}),o(f,{cols:"2"},{default:d(()=>[o(O,{text:"下載報表"},{activator:d(({props:r})=>[o(K,q({class:"ml-3 float-right",disabled:e.list.length<1,icon:"mdi-file-export"},r,{size:"small",color:"purple-darken-3",onClick:A(i,["prevent"])}),null,16,["disabled"])]),_:1})]),_:1}),o(f,{cols:"6"})]),_:1}),o(T,{dense:""},{default:d(()=>[o(f,{cols:"12"},{default:d(()=>[o(de,null,{default:d(()=>[n("thead",null,[n("tr",null,[n("th",ke,c(l.value.courtType),1),n("th",we,c(l.value.typeId),1),n("th",ye,c(l.value.num),1),n("th",xe,c(l.value.judgeDate),1),n("th",Ee,c(l.value.fileNumber),1),n("th",Pe,c(l.value.createdAtText),1),Te])]),n("tbody",null,[(k(!0),I(ne,null,ie(e.list,(r,S)=>(k(),I("tr",{key:S},[n("td",Re,c(r.courtTypeTitle),1),n("td",Ve,c(r.typeTitle),1),n("td",Ae,c(r.caseInfo),1),n("td",je,c(r.judgeDateText),1),n("td",Ce,c(r.fileNumber),1),n("td",Be,[o(O,{text:"查看審核紀錄"},{activator:d(({props:x})=>[n("a",q({href:"#",ref_for:!0},x,{onClick:A($=>B(r.id),["prevent"])}),c(r.reviewdAtText),17,Se)]),_:2},1024)]),n("td",$e,[r.modifyRecords&&r.modifyRecords.length?(k(),N(O,{key:0,text:"查看下載紀錄"},{activator:d(({props:x})=>[n("a",q({class:"pl-3",href:"#",ref_for:!0},x,{onClick:A($=>y(r),["prevent"])}),c(r.modifyRecords.length),17,Ie)]),_:2},1024)):J("",!0)])]))),128))])]),_:1})]),_:1}),o(f,{cols:"12"},{default:d(()=>[n("span",{class:"ml-3",textContent:c(e.comments)},null,8,Oe)]),_:1})]),_:1}),o(oe,{persistent:"",modelValue:e.datePicker.active,"onUpdate:modelValue":a[4]||(a[4]=r=>e.datePicker.active=r),width:W(j).S+50},{default:d(()=>[e.datePicker.active?(k(),N(fe,{key:0,"max-width":W(j).S},{default:d(()=>[o(s,{title:e.datePicker.title,onCancel:g},null,8,["title"]),o(ce,null,{default:d(()=>[o(T,null,{default:d(()=>[o(f,{cols:"12"},{default:d(()=>[o(_,{ref_key:"period_picker",ref:p,roc:e.datePicker.roc,labels:e.datePicker.labels,dates:e.datePicker.dates,values:e.datePicker.values},null,8,["roc","labels","dates","values"])]),_:1})]),_:1}),o(T,{dense:""},{default:d(()=>[o(f,{cols:"12"},{default:d(()=>[o(K,{onClick:A(D,["prevent"]),color:"success",class:"float-right"},{default:d(()=>[ue(" 確定 ")]),_:1})]),_:1})]),_:1})]),_:1})]),_:1},8,["max-width"])):J("",!0)]),_:1},8,["modelValue","width"])])}}};export{We as default};