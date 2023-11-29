using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class ArmyManager : MonoBehaviour
{

     public enum ArmyType
     {
          Player,
          Zombie,
     }
     private static Dictionary<ArmyType, ArmyManager> _managers = new Dictionary<ArmyType, ArmyManager>();

     [SerializeField] private ArmyType _type;
     [SerializeField] private string _armyTag;
     [SerializeField] private List<ArmyType> _enemies = new List<ArmyType>();
     
     private List<GameObject> _units;

     private void Awake()
     {
          _units = GameObject.FindGameObjectsWithTag(_armyTag).ToList();
          _units.ForEach(obj => obj.GetComponent<Unit>().manager = this);
          _managers.Add(_type, this);
     }

     public List<GameObject> GetAllUnits() => _units;
     public List<GameObject> GetAllEnemyUnits() => _managers
          .Where(obj => _enemies.Contains(obj.Key))
          .Select(obj => obj.Value.GetAllUnits())
          .SelectMany(list => list)
          .ToList();

     public GameObject GetNearestEnemyUnit(Vector3 position) => GetNearestUnitOfList(GetAllEnemyUnits(), position);
     public GameObject GetNearestUnit(Vector3 position) => GetNearestUnitOfList(_units, position);
     public void RemoveUnit(GameObject unit)
     {
          if (_units.FindIndex(obj => obj == unit) is int index && index != -1) _units.RemoveAt(index);
     }

     public static GameObject GetNearestUnitOfType(ArmyType type, Vector3 position) => _managers[type].GetNearestUnit(position);
     public static List<GameObject> GetAllUnitsOfType(ArmyType type) => _managers[type].GetAllUnits();


     private static GameObject GetNearestUnitOfList(List<GameObject> listOfUnits, Vector3 position)
     {
          GameObject result = null;
          float distance = float.MaxValue;

          foreach(var unit in listOfUnits)
          {
               if (distance >= Vector3.Distance(unit.transform.position, position))
               {
                    distance=Vector3.Distance(unit.transform.position, position);
                    result = unit;
               }
          }

          return result;
     }
}
