export class Service {


  public static TYPES: string[] = ['varchar', 'int', 'date',"bool","bigint","foreign key"];
  public static get ENABLE() {
    return{
      number:["int","bigint","varchar","bool"],
      int:["int","bigint"],
      date:["date"],
      base:[]
    }
  }

  public static get SERVICEMODE() {
    return {
      publish: {
        view: false
      },
      service: {
        view: true
      },
      container: {
        view: true
      },
      visualization: {
        view: false
      }
    }
  }

  public static get TABLEMODE() {
    return {
      service: ["name", "type", "idenv", "container_name"," container_service", "servicecategory_description", "servicecategory_name", "tenant_name","tenant_site","tenant_area"],
      tenant: ["name", "site", "area"],
      container:["name", "service"],
      application:["name", "port", "url", "datacenter_name", "datacenter_site", "userprofile_lastname", "userprofile_password", "userprofile_username"],
      datacenter: ["name", "site", "dimension"],
      servicecategory: ["name", "description"],
      userprofile:["firstname", "lastname", "username", "password"]
    }
  }

  public static SERVICEMODE2: any[] =
    [{
      service: 'userprofile',
      view: false,
      field: ["firstname", "lastname", "username", "password"],
      index: 0
    },
    {
      service: 'service',
      view: false,
      field: ["name", "type", "idenv", "container_id", "tenant_id", "servicecategory_id"],
      index: 1
    },
    {
      service: 'container',
      view: false,
      field: ["name", "service"],
      index: 2
    },
    {
      service: 'application',
      view: false,
      field: ["name", "port", "url", "userprofile_id"],
      index: 3
    },
    {
      service: 'datacenter',
      view: false,
      field: ["name", "site", "dimension"],
      index: 4
    },
    {
      service: 'tenant',
      view: false,
      field: ["name", "site", "area"],
      index: 5
    },
    {
      service: 'servicecategory',
      view: false,
      field: ["name", "description"],
      index: 6
    }, {
      service: 'organigram',
      view: false,
      field: ["id", "leader", "worker"],
      index: 7
    },
    {
      service: 'rediskey',
      view: false,
      field: ["id", "key"],
      index: 8
    }
    ]
}
