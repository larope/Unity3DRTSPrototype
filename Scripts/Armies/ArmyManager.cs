using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ArmyManager : MonoBehaviour
{

     public enum ArmyType
     {
          Player,
          Zombie
     }
     public static Dictionary<ArmyType, ArmyManager> managers = new Dictionary<ArmyType, ArmyManager>();

     [SerializeField] private ArmyType _type;
     [SerializeField] private string _armyTag;
     [SerializeField] private List<ArmyType> _enemies = new List<ArmyType>();
     
     private List<GameObject> _unitsGO;
     private List<Unit> _units;

     private void Start()
     {
          _unitsGO = GameObject.FindGameObjectsWithTag(_armyTag).ToList();
          _units = _unitsGO
               .Select(obj => obj.GetComponent<Unit>())
               .ToList();
          managers.Add(_type, this);
     }

     public List<GameObject> GetAllUnits() => _unitsGO;
     public List<GameObject> GetAllEnemyUnits() => managers
          .Where(obj => _enemies.Contains(obj.Key))
          .Select(obj => obj.Value.GetAllUnits())
          .SelectMany(list => list)
          .ToList();
     public GameObject GetNearestUnit(Vector3 position)
     {
          GameObject result = new GameObject();
          float distance = float.MaxValue;
          foreach(var unit in _unitsGO)
          {
               if (distance >= Vector3.Distance(unit.transform.position, position))
               {
                    distance=Vector3.Distance(unit.transform.position, position);
                    result = unit;
               }
          }

          return result;
     }

     public void RemoveUnit(Unit unit)
     {
          int index = _units.FindIndex(obj => obj == unit);
          _units.RemoveAt(index);
          _unitsGO.RemoveAt(index);
     }     
     
     public static GameObject GetNearestUnitOfType(Vector3 position, ArmyType type)
     {
          GameObject result = new GameObject();
          float distance = float.MaxValue;
          foreach(var unit in GetAllUnitsOfType(type))
          {
               if (distance >= Vector3.Distance(unit.transform.position, position))
               {
                    distance=Vector3.Distance(unit.transform.position, position);
                    result = unit;
               }
          }

          return result;
     }
     public static List<GameObject> GetAllUnitsOfType(ArmyType type) => managers[type].GetAllUnits();
     
}
