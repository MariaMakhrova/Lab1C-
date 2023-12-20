import Vue from "vue";
import VueRouter from "vue-router";
import UniversityList from "@/components/UniversityList.vue";
import UniversityDetails from "@/components/UniversityDetails.vue";
import UniversityForm from "@/components/UniversityForm.vue";

Vue.use(VueRouter);

const routes = [
  { path: "/", component: UniversityList, name: "university-list" },
  { path: "/details/:id", component: UniversityDetails, name: "university-details" },
  { path: "/create", component: UniversityForm, name: "create-university" },
];

const router = new VueRouter({
  routes,
  mode: "history", 
});

export default router;