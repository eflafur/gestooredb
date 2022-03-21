export class DataModel {
    public static SERVICEMODE1: any[] =
    [{
      service: 'userprofile',
      view: false,
      field: ["firstname", "lastname", "username", "password"],
      index: 0
    },
    {
      service: 'service',
      view: false,
      field: ["name", "type", "idenv", "container_name", "tenant_site", "servicecategory_description", "servicecategory_name", "tenant_name"],
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
      field: ["name", "port", "url", "datacenter_name", "datacenter_site", "userprofile_lastname", "userprofile_password", "userprofile_username"],
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
    ];

}