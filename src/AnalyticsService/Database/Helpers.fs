namespace AnalyticsService.Database.Helpers

open System
open System.ComponentModel
open System.Linq
open System.Reflection
open AnalyticsService.Database.Domain
open Dapper

module DbTypeHelper =
    let getDescriptionFromAttr (memberInfo: MemberInfo) =
        if memberInfo = null then
            None
        else
            let attr =
                Attribute.GetCustomAttribute(memberInfo, typeof<DescriptionAttribute>, false)
                :?> DescriptionAttribute
            match attr <> null with
            | true -> Some(attr.Description)
            | false -> None
    
    let getPropertyMap<'T> =
        CustomPropertyTypeMap(
            typeof<'T>,
            fun i columnName -> i.GetProperties().FirstOrDefault(
                fun prop -> getDescriptionFromAttr(prop).Value = columnName.ToLower()
            )
        )

    /// Method for setting maps for entities.
    let SetTypeMap =
        SqlMapper.SetTypeMap(typeof<BatchStat>, getPropertyMap<BatchStat>)
